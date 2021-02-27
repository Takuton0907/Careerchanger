using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropArea : MonoBehaviour, IDropHandler
{
    public AttackMode weapon = 0;

    [SerializeField]
    string m_tochSe = "sound_ok";

    public void OnDrop(PointerEventData data)
    {
        AudioManager.Instance.PlaySE(m_tochSe);
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