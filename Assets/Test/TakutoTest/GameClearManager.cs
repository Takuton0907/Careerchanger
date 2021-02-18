using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    [Header("花を取った後のフェードする時間"), SerializeField]
    float m_flowerGetFadeTime = 5;

    [SerializeField] string m_clearBgm = "Future_Girl";

    string m_clearSe = "Clear";

    //ゲームクリア時のコールバック
    public delegate void GameClearEvent();
    GameClearEvent gameClearCallBack;
    public event GameClearEvent AddGameClearEvent
    {
        add { gameClearCallBack += value; }
        remove { gameClearCallBack -= value; }
    }

    GameClearCon m_gameClearCon;
    ClearPerformancesCon m_clearPerformancesCon;

    private void Awake()
    {
        GetGameClearEvents();
        m_gameClearCon = FindObjectOfType<GameClearCon>();
        m_clearPerformancesCon = FindObjectOfType<ClearPerformancesCon>();
    }

    private void Start()
    {
        LevelManager.Instance.GameClearManager = this;
    }

    /// <summary> ゲームクリア時の処理をEventに登録 </summary>
    private void GetGameClearEvents()
    {
        IClearEvent[] gameoverEvents = GameObjectExtensions.FindObjectsOfInterface<IClearEvent>();
        foreach (var item in gameoverEvents)
        {
            AddGameClearEvent += item.Clear;
        }
    }

    /// <summary> 花を獲得した後の演出を管理しています </summary>
    public void GameClear()
    {
        AudioManager.Instance.PlayBGM(m_clearBgm);
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        gameClearCallBack?.Invoke();

        DataManager.Instance.StageClear();

        if (m_gameClearCon != null)
        {
            yield return m_gameClearCon.InstanceGameClearUI();
        }
        else yield return null;
    } 
}
