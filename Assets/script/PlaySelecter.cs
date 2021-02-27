using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySelecter : MonoBehaviour
{
    [SerializeField]
    string m_tochSeName = "sound_no";

    private void Awake()
    {
        transform.localScale = Vector2.zero;
    }

    public void SelecterFadeIn()
    {
        StartCoroutine(Fadein());
    }

    public void SelecterFadeOut()
    {
        AudioManager.Instance.PlaySE(m_tochSeName);
        StartCoroutine(FadeOut());
    }

    IEnumerator Fadein()
    {
        var size = 0.0f;
        var speed = 0.05f;

        while (size <= 1.0f)
        {
            transform.localScale = Vector2.Lerp(new Vector2(0, 0), new Vector2(1, 1), size);
            size += speed;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        var size = 1f;
        var speed = 0.05f;

        while (size >= 0)
        {
            transform.localScale = Vector2.Lerp(new Vector2(0, 0), new Vector2(1, 1), size);
            size -= speed;
            yield return null;
        }

        transform.localScale = Vector2.zero;
    }
}
