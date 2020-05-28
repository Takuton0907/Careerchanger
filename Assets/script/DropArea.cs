using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Weapons weapon = 0;
    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        DragObj dragObj = data.pointerDrag.GetComponent<DragObj>();
        if (dragObj != null && dragObj.weapon == weapon || dragObj != null && weapon == Weapons.Init)
        {
            dragObj.parentTransform = this.transform;
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
        }
    }
}