using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        DragObj dragObj = data.pointerDrag.GetComponent<DragObj>();
        if (dragObj != null)
        {
            dragObj.parentTransform = this.transform;
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
        }
    }
}