
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindwCon : MonoBehaviour
{
    [SerializeField] Button m_retryButton;
    [SerializeField] Button m_giveupButton;

    CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0;
        m_canvasGroup.blocksRaycasts = false;

        m_retryButton.gameObject.SetActive(false);
        m_giveupButton.gameObject.SetActive(false);
    }

    public void GameOverWindowOpne()
    {
        m_canvasGroup.blocksRaycasts = true;
        StartCoroutine(InstaceGameOverUI(2));
    }

    //雑なべた書きでフェードを実装しています。
    IEnumerator InstaceGameOverUI(float interval)
    {
        float time = 0;
        while (time <= interval)
        {
            m_canvasGroup.alpha = time / interval;
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        m_retryButton.gameObject.SetActive(true);
        m_giveupButton.gameObject.SetActive(true);
    }
}
