using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AttackMode weapon = 0;
    public Transform parentTransform;
    GameObject copyObj;
    DragObj CopyDragObj;

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag");
        copyObj = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, transform.parent);
        copyObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprite/" + (weapon).ToString() + "Sprite");
        copyObj.GetComponent<Button>().enabled = false;
        //copyObj.name = gameObject.name;

        copyObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copyObj.GetComponent<DragObj>().parentTransform = transform.parent;
        copyObj.transform.SetParent(transform.parent.parent.parent);

        copyObj.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        Debug.Log($"copyObj = { copyObj.GetComponent<RectTransform>().sizeDelta } {gameObject.GetComponent<RectTransform>().sizeDelta}");

    }
    public void OnDrag(PointerEventData data)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
        TargetPos.z = -1;
        copyObj.transform.position = TargetPos;
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (weapon == parentTransform.GetComponent<DropArea>().weapon || parentTransform.GetComponent<DropArea>().weapon == AttackMode.None)
        {
            Debug.Log("OnEndDrag");
            if (parentTransform.childCount >= 1)
            {
                foreach (Transform item in parentTransform)
                {
                    Destroy(item.gameObject);
                }
            }
            copyObj.transform.SetParent(parentTransform);
            copyObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            copyObj.transform.position = parentTransform.transform.position;
        }
        else
        {

        }
    }
}