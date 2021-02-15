using System.Collections;
using UnityEngine;

public class MainUICon : MonoBehaviour, IUiHandler
{
    CanvasGroup m_canvasGroup;

    [SerializeField]
    float m_fadeOutTime = 0.2f;

    [SerializeField]
    float m_fadeInTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator MainUIFadeIn()
    {
        float time = 0;
        while (m_canvasGroup.alpha < 1)
        {
            m_canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / m_fadeInTime);

            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MainUIFadeOut()
    {
        float time = 0;
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / m_fadeOutTime);

            time += Time.deltaTime;
            yield return null;
        }
    }

    public void Enable()
    {
        m_canvasGroup.blocksRaycasts = true;
        StartCoroutine(MainUIFadeIn());
    }

    public void Disable()
    {
        m_canvasGroup.blocksRaycasts = false;
        StartCoroutine(MainUIFadeOut());
    }
}
