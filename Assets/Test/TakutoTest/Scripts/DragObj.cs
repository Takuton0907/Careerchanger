using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Weapons weapon = 0;
    public Transform parentTransform;
    GameObject copyObj;
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag");
        copyObj = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, transform.parent);
        copyObj.name = gameObject.name;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        parentTransform = transform.parent;
        transform.SetParent(transform.parent.parent);
    }
    public void OnDrag(PointerEventData data)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
        TargetPos.z = -1;
        transform.position = TargetPos;
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (weapon == parentTransform.GetComponent<DropArea>().weapon || parentTransform.GetComponent<DropArea>().weapon == Weapons.Init)
        {
            Debug.Log("OnEndDrag");
            if (parentTransform.childCount >= 1)
            {
                foreach (Transform item in parentTransform)
                {
                    Destroy(item.gameObject);
                }
            }
            transform.SetParent(parentTransform);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.position = parentTransform.transform.position;
        }
        else
        {

        }
    }
}