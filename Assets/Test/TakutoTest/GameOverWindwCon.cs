using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindwCon : MonoBehaviour, IUiHandler
{
    [SerializeField] Button m_giveupButton;

    CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();

        m_giveupButton.interactable = false;

        Disable();
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

        m_giveupButton.interactable = true;
    }

    public void Enable()
    {
        m_canvasGroup.blocksRaycasts = true;
        StartCoroutine(InstaceGameOverUI(2));
    }

    public void Disable()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.blocksRaycasts = false;
    }
}
