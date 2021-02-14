using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComboCon : MonoBehaviour
{
    CanvasGroup m_canvasGroup;

    [SerializeField] float m_fadeSpeed = 2;

    void Start()
    {
        var scoreManager = LevelManager.Instance.ScoreManager;

        m_canvasGroup = GetComponent<CanvasGroup>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Text text))
            {
                if (scoreManager == null) return;

                scoreManager.m_comboText = text;
                scoreManager.ComboReset();
                scoreManager.ComboResetCallback = () =>
                {
                    StartCoroutine(FadeOut());
                };
                scoreManager.ComboCallback = () =>
                {
                    StartCoroutine(FadeIn());
                };
            }
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha -= Time.deltaTime * m_fadeSpeed;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        while (m_canvasGroup.alpha < 1)
        {
            m_canvasGroup.alpha += Time.deltaTime * m_fadeSpeed * 10;
            yield return null;
        }
    }
}
