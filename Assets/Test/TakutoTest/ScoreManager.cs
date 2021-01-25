using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("敵撃破による獲得スコア"), SerializeField] 
    int m_enemyDefeatPoint = 100;

    [Header("残っている時間による1秒ごとの獲得スコア"), SerializeField]
    int m_remainingTime = 50;

    [Header("最大時間"), SerializeField]
    float m_maxTime = 120;
    float m_timer;

    /// <summary> 経過時間 </summary>
    public float ElapsedTime { get { return m_timer; } }
    /// <summary> 敵の撃破数 </summary>
    public int DefeatedEnemies { get; private set; }
    /// <summary> ダメージを受けた回数 </summary>
    public int DamageAffectedNum { get; private set; }
    /// <summary> 残り時間によるスコアの合計 </summary>
    public int RemainingTimeScore { get; private set; }
    /// <summary> 敵撃破によるスコアの合計 </summary>
    public int AllEnemyDefeatedScore { get; private set; }
    /// <summary> ノーダメージ時のボーナスを返します </summary>
    public int NoDamageClearScore
    {
        get
        {
            if (DamageAffectedNum != 0)
            {
                return 0;
            }
            else
            {
                return DataManager.Instance.GetScoreRank().NoDamageBonus;
            }
        }
    }
    /// <summary> 最終評価 </summary>
    public int Score { private set; get; } = 0;
    /// <summary> 最終的なランク </summary>
    public StageClearRank StageClearRank { private set; get; }

    //Timer m_timerCon = null;

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        LevelManager.Instance.ScoreManager = this;
        m_timer = m_maxTime;

        //m_timerCon = FindObjectOfType<Timer>();
        //m_timerCon?.TimerUpdate(m_maxTime);
    }

    /// <summary> スコアの初期化 </summary>
    public void Setup()
    {
        m_timer = m_maxTime;
        DefeatedEnemies = 0;
        DamageAffectedNum = 0;
    }
    /// <summary> スタートからの時間のカウント </summary>
    public void TimeCount(float time)
    {
        m_timer -= time;
        //m_timerCon?.TimerUpdate(m_timer);
    }
    /// <summary> 倒した敵のカウント </summary>
    public void KillEnemyCount(int enemyCount)
    {
        if (DefeatedEnemies + enemyCount <= 0) DefeatedEnemies = 0;
        else DefeatedEnemies += enemyCount;
    }
    /// <summary> ダメージを受けた回数 </summary>
    public void DamageAffected(int damageCount)
    {
        if (DamageAffectedNum + damageCount <= 0) DamageAffectedNum = 0;
        else DamageAffectedNum += damageCount;
    }
    /// <summary> スコアの計算結果を返します </summary>
    private int GetScore()
    {
        ScoreRank scoreData = DataManager.Instance.GetScoreRank();

        Score = 0;

        int enemyKillScore = DefeatedEnemies * m_enemyDefeatPoint;
        Score += enemyKillScore; //敵撃破によるスコアの計算
        AllEnemyDefeatedScore = enemyKillScore;
        if (DefeatedEnemies >= LevelManager.Instance.MoveObjectManager.TotalEnemyCount)
        {
            Score += scoreData.AllEnmeyDefeatBonus;
        }

        int rematingTimeScore = (int)m_timer * m_remainingTime; //時間によるスコアの獲得
        RemainingTimeScore = rematingTimeScore;
        Score += rematingTimeScore;

        if (DamageAffectedNum <= 0)
        {
            Score += scoreData.NoDamageBonus;
        }

        return Score;
    }
    /// <summary> 計算したスコアに基づいてランクを返します </summary>
    private StageClearRank GetStageClearRank()
    {
        ScoreRank scoreData = DataManager.Instance.GetScoreRank();

        int score = GetScore();

        if (score >= scoreData.S_Rank)
        {
            StageClearRank = StageClearRank.S;
            return StageClearRank.S;
        }
        else if (score >= scoreData.A_Rank)
        {
            StageClearRank = StageClearRank.A;
            return StageClearRank.A;
        }
        else 
        {
            StageClearRank = StageClearRank.B;
            return StageClearRank.B;
        }
    }
    /// <summary> リザルトに出すスコアを計算します </summary>
    public void Resalt()
    {
        GetStageClearRank();
    }
}