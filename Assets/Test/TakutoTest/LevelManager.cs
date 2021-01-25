
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
    private MoveObjectManager m_moveObjectManager;
    public MoveObjectManager MoveObjectManager
    {
        get { return m_moveObjectManager; }
        set { if (m_moveObjectManager == null) m_moveObjectManager = value; }
    }

    private LifeManager m_lifeManager;
    public LifeManager LifeManager
    {
        get { return m_lifeManager; }
        set {if (m_lifeManager == null){ m_lifeManager = value; }}
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
                    m_moveObjectManager.MoveObjectsInit();
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
                m_moveObjectManager?.ManagedUpdate();
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
        m_backGroundManager?.RepeatedlyMove();
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
                m_moveObjectManager?.AllStop();
                m_moveObjectManager?.AllEnemyAnimationOnlyStart();
                break;
            case LevelState.Play:
                m_moveObjectManager?.AllMove();
                break;
            case LevelState.Tutorial:
                m_moveObjectManager?.AllStop();
                break;
            case LevelState.Stop:
                m_moveObjectManager?.AllStop();
                break;
            case LevelState.GameOver:
                m_gameOverManager?.GameOver();
                break;
            case LevelState.Clear:
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
}