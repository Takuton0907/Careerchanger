﻿
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    /// <summary>Level全体のState</summary>
    enum LevelState
    {
        Init,
        Start,
        Play,
        Stop,
        Tutorial,
        GameOver,
        Clear,
        Result,
    }
    //レベルの進行管理
    LevelState levelState = LevelState.Init;

    //以下他のManagerクラスの参照
    private EnemyManager m_enemyManager;
    public EnemyManager EnemyManager
    {
        get { return m_enemyManager; }
        set { if (m_enemyManager == null) m_enemyManager = value; }
    }

    private LevelIntervalCon m_intervalCon;
    public LevelIntervalCon LevelIntervalCon
    {
        get { return m_intervalCon; }
        set { if (m_intervalCon == null) m_intervalCon = value; }
    }

    private PlayerCon m_playerCon;
    public PlayerCon PlayerCon
    {
        get { return m_playerCon; }
        set { if (m_playerCon == null) m_playerCon = value; }
    }

    private GameClearManager m_gameClearManager;
    public GameClearManager GameClearManager
    {
        get { return m_gameClearManager; }
        set { if (m_gameClearManager == null) m_gameClearManager = value; }
    }

    private GameOverManager m_gameOverManager;
    public GameOverManager GameOverManager
    {
        get { return m_gameOverManager; }
        set { if (m_gameOverManager == null) m_gameOverManager = value; }
    }

    private ScoreManager m_scoreManager;
    public ScoreManager ScoreManager 
    { 
        get { return m_scoreManager; }
        set { if (m_scoreManager == null) m_scoreManager = value; }
    }

    private EffectManager m_effectManager;
    public EffectManager EffectManager
    {
        get { return m_effectManager; }
        set { if (m_effectManager == null) m_effectManager = value; }
    }

    private BackGroundManager m_backGroundManager;
    public BackGroundManager BackGroundManager
    {
        get { return m_backGroundManager; }
        set { if (m_backGroundManager == null) m_backGroundManager = value; }
    }

    //private TutorialManager m_tutorialManager;

    private new void Awake()
    {
        base.Awake();
        StageInstance();
        //m_tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Start()
    {
        StateChange(LevelState.Init); 
        AudioManager.Instance.PlayBGM(DataManager.Instance.GetStage().stageBgmName);
    }

    private void Update()
    {
        switch (levelState)
        {
            case LevelState.Init:
                if (!FadeManager.Instance.isFading)
                {
                    StateChange(LevelState.Start);
                    m_enemyManager.EnemySetup();

                    //m_playerCon?.Init();
                }
                break;
            case LevelState.Start:
                if (m_intervalCon == null) StateChange(LevelState.Play);
                if (m_intervalCon.CountDown())
                {
                    StateChange(LevelState.Play);
                }
                break;
            case LevelState.Play:
                //m_tutorialManager?.ManagedUpdate();
                m_enemyManager?.ManagedUpdate();
                //m_playerCon?.ManagedUpdate();
                m_effectManager?.ManagedUpdate();
                m_scoreManager?.TimeCount(Time.unscaledDeltaTime);
                m_backGroundManager?.BackgroundMove();

                if (ScoreManager.ElapsedTime <= 0)
                {
                    GameOver();
                }
                break;
            case LevelState.Tutorial:
                m_effectManager?.ManagedUpdate();
                break;
            case LevelState.Stop:
                break;
            case LevelState.GameOver:
                break;
            case LevelState.Clear:
                break;
            case LevelState.Result:
                break;
            default:
                break;
        }
        m_backGroundManager?.CloudMove();
    }

    /// <summary> ステージのインスタンス </summary>
    private void StageInstance()
    {
        StageData stageData = DataManager.Instance.GetStage();
        if (stageData.stage != null)
        {
            Instantiate(stageData.stage);
        }
        else
        {
            Debug.LogWarning("ステージが正しく読み込めませんでした。");
        }
    }
    /// <summary> state切り替え </summary>
    private void StateChange(LevelState nextState)
    {
        //今のstateから抜けるときの処理
        switch (levelState)
        {
            case LevelState.Init:
                m_scoreManager?.Setup();
                //m_tutorialManager?.TutorialEventSet();
                break;
            case LevelState.Start:
                //m_playerCon?.Play();
                break;
            case LevelState.Play:
                break;
            case LevelState.Tutorial:
                break;
            case LevelState.Stop:
                break;
            case LevelState.GameOver:
                break;
            case LevelState.Clear:
                break;
            case LevelState.Result:
                break;
            default:
                break;
        }

        levelState = nextState;
        Debug.Log($"LevelManagerのStateが{levelState}に変更");

        //次のstateに入った時の処理
        switch (levelState)
        {
            case LevelState.Init:
                break;
            case LevelState.Start:
                m_enemyManager?.AllEnemyStop();
                m_enemyManager.AllEnemyAnimationOnlyStart();
                break;
            case LevelState.Play:
                m_enemyManager?.AllEnemyMove();
                //m_playerCon?.MoveStart();
                break;
            case LevelState.Tutorial:
                m_enemyManager?.AllEnemyStop();
                //m_playerCon?.MoveStop();
                break;
            case LevelState.Stop:
                m_enemyManager?.AllEnemyStop();
                //m_playerCon?.MoveStop();
                break;
            case LevelState.GameOver:
                m_gameOverManager?.GameOver();
                //m_playerCon.GameOver();
                break;
            case LevelState.Clear:
                //m_playerCon.Clear();
                break;
            case LevelState.Result:
                ScoreManager.Resalt();
                m_gameClearManager?.GameClear();
                break;
            default:
                break;
        }
    }

    /// <summary> ゲームオーバー </summary>
    public void GameOver() => StateChange(LevelState.GameOver);
    /// <summary> ゲームクリア </summary>
    public void GameClear() => StateChange(LevelState.Clear);
    /// <summary> リザルトへの切り替え </summary>
    public void GameResalt() => StateChange(LevelState.Result);
    /// <summary> ゲームを一時停止する </summary>
    public void GameStop() => StateChange(LevelState.Stop);
    /// <summary> 一時停止を解除する </summary>
    public void GamePlay() => StateChange(LevelState.Play);
    /// <summary> チュートリアルの開始 </summary>
    public void TutorialPlay() => StateChange(LevelState.Tutorial);
    /// <summary> TimeScalの変更 </summary>
    public void ChangeTimeScale(TimeScaleType type)
    {
        switch (type)
        {
            case TimeScaleType.Nomal:
                Time.timeScale = 1;
                break;
            case TimeScaleType.Zero:
                Time.timeScale = 0;
                break;
            case TimeScaleType.LineAction:
                Time.timeScale = 0.3f;
                break;
        }
    }
}

public enum TimeScaleType
{
    /// <summary> TimeScal = 1 </summary>
    Nomal,
    /// <summary> TimeScal = 0 </summary>
    Zero,
    /// <summary> 線を引いている最中のTimeScal </summary>
    LineAction,
}