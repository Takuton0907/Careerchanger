using System.Collections;
using UnityEngine;

public class GameClearCon : MonoBehaviour
{
    CanvasGroup m_scoerGroup;

    private void Awake()
    {
        m_scoerGroup = GetComponent<CanvasGroup>();
        m_scoerGroup.alpha = 0;
        m_scoerGroup.blocksRaycasts = false;
    }

    public IEnumerator InstanceGameClearUI(float interval)
    {
        ScoreManager scoreManager = LevelManager.Instance.ScoreManager;

        scoreManager.Resalt();

        interval = 1;
        float time = 0;
        while (time <= interval)
        {
            m_scoerGroup.alpha = Mathf.Lerp(0f, 1f, time / interval);

            time += Time.deltaTime;
            yield return null;
        }

        interval = 1;
        time = 0;
        while (time <= interval)
        {
            time += Time.deltaTime;
            yield return null;
        }

        switch (scoreManager.StageClearRank)
        {
            case StageClearRank.A:
                break;
            case StageClearRank.B:
                break;
            case StageClearRank.C:
                break;
        }

        m_scoerGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeInStageClearText()
    {
        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            time += Time.deltaTime;
            yield return null;
        }

        m_scoerGroup.blocksRaycasts = true;
    }
    public IEnumerator FadeOutStageClearText()
    {
        yield return new WaitForSeconds(3);

        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
}
