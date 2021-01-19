using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearCon : MonoBehaviour
{
    CanvasGroup m_scoerGroup;

    [SerializeField] Text m_remainingTimerText;

    [SerializeField] Text m_deathEnemyNumberText;
    
    [SerializeField] Text m_noDamageBonusText;

    [SerializeField] Text m_allEnemydefatedBounusText;
    
    [SerializeField] Text m_totalScoreText;
    
    [SerializeField] Image m_clearRankText;

    [SerializeField] Sprite[] m_ranks;

    [SerializeField] Button m_mapSelectBackButton;

    [SerializeField] Image m_stageClearTextImage;

    private void Awake()
    {
        m_mapSelectBackButton.gameObject.SetActive(false);

        m_scoerGroup = GetComponent<CanvasGroup>();

        m_scoerGroup.alpha = 0;
        m_scoerGroup.blocksRaycasts = false;

        m_clearRankText.color = Color.clear;
        m_stageClearTextImage.color = Color.clear;
    }

    public IEnumerator InstanceGameClearUI(float interval)
    {
        ScoreManager scoreManager = LevelManager.Instance.ScoreManager;

        scoreManager.Resalt();

        m_remainingTimerText.text = scoreManager.RemainingTimeScore.ToString();
        m_deathEnemyNumberText.text = scoreManager.DefeatedEnemies.ToString();
        m_noDamageBonusText.text = scoreManager.NoDamageClearScore.ToString();
        m_allEnemydefatedBounusText.text = scoreManager.AllEnemyDefeatedScore.ToString();

        m_totalScoreText.text = scoreManager.Score.ToString();

        m_clearRankText.sprite = m_ranks[(int)scoreManager.StageClearRank];

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
            m_clearRankText.color = new Color(Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval));

            time += Time.deltaTime;
            yield return null;
        }

        switch (scoreManager.StageClearRank)
        {
            case StageClearRank.S:
                break;
            case StageClearRank.A:
                break;
            case StageClearRank.B:
                break;
        }

        m_scoerGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1);

        m_mapSelectBackButton.gameObject.SetActive(true);
    }

    public IEnumerator FadeInStageClearText()
    {
        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            m_stageClearTextImage.color = new Color(Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval), Mathf.Lerp(0f, 1f, time / interval));

            time += Time.deltaTime;
            yield return null;
        }

        m_scoerGroup.blocksRaycasts = true;
        m_stageClearTextImage.color = Color.white;
    }
    public IEnumerator FadeOutStageClearText()
    {
        yield return new WaitForSeconds(3);

        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            m_stageClearTextImage.color = new Color(Mathf.Lerp(1f, 0f, time / interval), Mathf.Lerp(1f, 0f, time / interval), Mathf.Lerp(1f, 0f, time / interval), Mathf.Lerp(1f, 0f, time / interval));

            time += Time.deltaTime;
            yield return null;
        }
        m_stageClearTextImage.color = Color.clear;
        m_stageClearTextImage.gameObject.SetActive(false);
    }
}
