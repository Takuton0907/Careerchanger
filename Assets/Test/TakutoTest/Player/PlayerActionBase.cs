using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerActionBase : MonoBehaviour
{
    IEnumerator m_actonCoroutin;

    abstract protected IEnumerator PlayerAction(float piece);

    /// <summary> 初めからの実行 </summary>
    public void RunAction(float piece)
    {
        if (m_actonCoroutin != null)
        {
            StopCoroutine(m_actonCoroutin);
        }
        m_actonCoroutin = PlayerAction(piece);
        StartCoroutine(m_actonCoroutin);
    }
    /// <summary> Actionの停止(一時停止も可能) </summary>
    public void StopAction()
    {
        StopCoroutine(m_actonCoroutin);
    }
    /// <summary> Actionの再開 </summary>
    public void RestartAction()
    {
        StartCoroutine(m_actonCoroutin);
    }
}