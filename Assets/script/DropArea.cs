using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropArea : MonoBehaviour, IDropHandler
{
    public AttackMode weapon = 0;
    public void OnDrop(PointerEventData data)
    {
        StartCoroutine(Drop(data));
    }

    IEnumerator Drop(PointerEventData data)
    {
        var dragObj = data.pointerDrag.GetComponent<DragObj>();

        if (dragObj != null && dragObj.weapon == weapon || dragObj != null && weapon == AttackMode.None)
        {
            dragObj.copyObj.GetComponent<DragObj>().parentTransform = this.transform;
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
        }
        yield return null;
    }
}