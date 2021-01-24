using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerActionBase : MonoBehaviour
{
    IEnumerator m_actonCoroutin;
    abstract public IEnumerator PlayerAction(float piece);

    /// <summary> 初めからの実行 </summary>
    protected void RunAction(float piece)
    {
        if (m_actonCoroutin != null)
        {
            StopCoroutine(m_actonCoroutin);
        }
        m_actonCoroutin = PlayerAction(piece);
        StartCoroutine(m_actonCoroutin);
    }
    /// <summary> Actionの停止(一時停止も可能) </summary>
    protected void StopAction()
    {
        StopCoroutine(m_actonCoroutin);
    }
    /// <summary> Actionの再開 </summary>
    protected void RestartAction()
    {
        StartCoroutine(m_actonCoroutin);
    }
}
