using UnityEngine;
using UnityEngine.UI;

public class ScoreCon : MonoBehaviour
{
    void Start()
    {
        var scoreManager = LevelManager.Instance.ScoreManager;

        if (TryGetComponent(out Text text))
        {
            if (scoreManager == null) return;

            scoreManager.m_scoreText = text;
            scoreManager.AddScore(0);
        }
    }
}
