﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    [Header("花を取った後のフェードする時間"), SerializeField] 
    float m_flowerGetFadeTime = 5;

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
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        gameClearCallBack?.Invoke();

        DataManager.Instance.StageClear();

        //StartCoroutine(FindObjectOfType<MainUICon>().MainUIFadeOut());

        if (m_clearPerformancesCon != null)
        {
            AudioManager.Instance.PlaySE(m_clearSe);
            yield return m_clearPerformancesCon.GetFlower(m_flowerGetFadeTime);
            StartCoroutine(m_gameClearCon.FadeInStageClearText());
            StartCoroutine(m_gameClearCon.FadeOutStageClearText());
        }
        else yield return null;

        if (m_gameClearCon != null) yield return m_gameClearCon.InstanceGameClearUI();
        else yield return null;
    }
}
