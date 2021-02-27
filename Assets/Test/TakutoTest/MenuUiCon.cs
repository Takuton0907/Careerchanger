using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiCon : MonoBehaviour, IUiHandler
{
    CanvasGroup m_canvasGroup;

    [SerializeField]
    float m_fadeOutTime = 0.2f;

    [SerializeField]
    float m_fadeInTime = 0.5f;

    [SerializeField]
    Transform m_frameTrans = null;

    private void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        Disable();
    }

    public void Disable()
    {
        m_canvasGroup.blocksRaycasts = false;
        StartCoroutine(FadeOut());
        StartCoroutine(CanvasGroupFadeOut());
    }

    public void Enable()
    {
        m_canvasGroup.blocksRaycasts = true;
        StartCoroutine(Fadein());
        StartCoroutine(CanvasGroupFadeIn());
    }

    IEnumerator Fadein()
    {
        float timer = 0;
        while (m_frameTrans.transform.localScale.magnitude < 1)
        {
            m_frameTrans.transform.localScale = Vector2.Lerp(new Vector2(0, 0), new Vector2(1, 1), timer / m_fadeInTime);
            timer += Time.deltaTime;
            Debug.Log("めにゅーの表示");
            yield return null;
        }

        m_frameTrans.localScale = Vector2.one;
    }

    IEnumerator FadeOut()
    {
        float timer = m_fadeOutTime;
        while (m_frameTrans.transform.localScale.magnitude > 0)
        {
            m_frameTrans.localScale = Vector2.Lerp(new Vector2(0, 0), new Vector2(1, 1), timer / m_fadeOutTime);
            timer -= Time.deltaTime;
            yield return null;
        }

        m_frameTrans.localScale = Vector2.zero;
    }

    IEnumerator CanvasGroupFadeOut()
    {
        float timer = m_fadeOutTime;
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha = timer / m_fadeOutTime;
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CanvasGroupFadeIn()
    {
        float timer = 0;
        while (m_canvasGroup.alpha < 1)
        {
            m_canvasGroup.alpha = timer / m_fadeInTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
