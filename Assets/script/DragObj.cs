using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AttackMode weapon = 0;
    public Transform parentTransform;
    public GameObject copyObj { private set; get; }

    Button m_mybutton = null;

    private void Start()
    {
        m_mybutton = GetComponent<Button>();
    }

    public void OnClickMyButtonDisable()
    {
        m_mybutton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag");
        copyObj = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, transform.parent);
        //copyObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprite/" + (weapon).ToString() + "Sprite");
        copyObj.GetComponent<Button>().enabled = false;
        //copyObj.name = gameObject.name;

        copyObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copyObj.transform.SetParent(transform.parent);

        copyObj.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = -1;
        copyObj.transform.position = TargetPos;
    }
    public void OnEndDrag(PointerEventData data)
    {
        StartCoroutine(EndDrag());
    }

    IEnumerator EndDrag()
    {
        yield return null; 
        var copyDrag = copyObj.GetComponent<DragObj>();
        if (copyDrag.parentTransform == null || copyDrag.parentTransform == default) 
        {
            Destroy(copyObj);
            yield break; 
        }
        if (copyDrag.parentTransform.TryGetComponent(out DropArea drop))
        {
            if (drop.weapon == weapon || drop.weapon == AttackMode.None)
            {
                Debug.Log("OnEndDrag");
                if (drop.transform.childCount >= 1)
                {
                    foreach (Transform item in copyDrag.parentTransform)
                    {
                        Destroy(item.gameObject);
                    }
                }
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                copyObj.transform.SetParent(copyDrag.parentTransform);
                copyObj.transform.localPosition = Vector3.zero;
            }
        }
    }
}