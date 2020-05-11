using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentTransform;
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        parentTransform = transform.parent;
        //transform.SetParent(transform.parent.parent);
    }
    public void OnDrag(PointerEventData data)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
        TargetPos.z = -1;
        transform.position = TargetPos;
    }
    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentTransform);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.position = parentTransform.transform.position;
    }
}