using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary> 敵の撃破数 </summary>
    public int DefeatedEnemies { get; private set; }
    /// <summary> 最終評価 </summary>
    public int Score { private set; get; } = 0;
    /// <summary> 最終的なランク </summary>
    public StageClearRank StageClearRank { private set; get; }
    /// <summary> 最大コンボ </summary>
    public int Combo { get; private set; } = 0;
    /// <summary> 現在のコンボ </summary>
    public int m_combo { get; private set; } = 0;

    private void Start()
    {
        LevelManager.Instance.ScoreManager = this;
    }
    private void Awake()
    {
        Setup();
    }

    /// <summary> 最大コンボを更新します </summary>
    public void ComboUpdate()
    {
        m_combo++;
        if (m_combo > Combo)
        {
            Combo = m_combo;
        }
    } 
    /// <summary> スコアの加算 </summary>
    public void AddScore(int value) => Score += value;
    /// <summary> スコアの計算結果を返します </summary>
    private int GetScore() => Score;
    /// <summary> スコアの初期化 </summary>
    public void Setup()
    {
        DefeatedEnemies = 0;
    }
    /// <summary> 倒した敵のカウント </summary>
    public void KillEnemyCount(int enemyCount)
    {
        if (DefeatedEnemies + enemyCount <= 0) DefeatedEnemies = 0;
        else DefeatedEnemies += enemyCount;
    }
    /// <summary> 計算したスコアに基づいてランクを返します </summary>
    private StageClearRank GetStageClearRank()
    {
        ScoreRank scoreData = DataManager.Instance.GetScoreRank();

        int score = GetScore();

        if (score >= scoreData.A_Rank)
        {
            StageClearRank = StageClearRank.A;
            return StageClearRank.A;
        }
        else if (score >= scoreData.B_Rank)
        {
            StageClearRank = StageClearRank.B;
            return StageClearRank.B;
        }
        else
        {
            StageClearRank = StageClearRank.C;
            return StageClearRank.C;
        }
    }
    /// <summary> リザルトに出すスコアを計算します </summary>
    public void Resalt()
    {
        GetStageClearRank();
    }
}