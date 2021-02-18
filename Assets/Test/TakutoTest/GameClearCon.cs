using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameClearCon : MonoBehaviour, IUiHandler
{
    CanvasGroup m_scoerGroup;

    [SerializeField] Text m_enemyText;
    [SerializeField] Text m_brakeText;
    [SerializeField] Text m_scoreText;
    [SerializeField] Image m_scoreImage;
    [SerializeField] Sprite[] m_scoreSprites;

    [SerializeField] Image m_clearImage = null;

    private void Awake()
    {
        m_scoerGroup = GetComponent<CanvasGroup>();
        Disable();
    }

    public IEnumerator InstanceGameClearUI()
    {
        float interval;
        ScoreManager scoreManager = LevelManager.Instance.ScoreManager;

        scoreManager.Resalt();
        m_enemyText.text = scoreManager.DefeatedEnemies.ToString("0");
        m_brakeText.text = scoreManager.Combo.ToString("0");
        m_scoreText.text = scoreManager.Score.ToString("0");

        switch (scoreManager.StageClearRank)
        {
            case StageClearRank.A:
                m_scoreImage.sprite = m_scoreSprites[0];
                break;
            case StageClearRank.B:
                m_scoreImage.sprite = m_scoreSprites[1];
                break;
            case StageClearRank.C:
                m_scoreImage.sprite = m_scoreSprites[2];
                break;
        }

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

        m_scoerGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeInStageClearText()
    {
        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            m_clearImage.color = new Color(1, 1, 1, time / interval);
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
            m_clearImage.color = new Color(1, 1, 1, time / interval);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void Enable()
    {
        StartCoroutine(InstanceGameClearUI());
    }

    public void Disable()
    {
        m_scoerGroup.alpha = 0;
        m_scoerGroup.blocksRaycasts = false;
    }
}
