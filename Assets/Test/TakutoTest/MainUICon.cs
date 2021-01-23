using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUICon : MonoBehaviour
{
    CanvasGroup m_canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator MainUIFadeIn()
    {
        float interval = 1;
        float time = 0;
        while (time <= interval)
        {
            m_canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / interval);

            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MainUIFadeOut()
    {
        float interval = 1;
        float time = 0;
        while (time <= interval)
        {
            m_canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / interval);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
