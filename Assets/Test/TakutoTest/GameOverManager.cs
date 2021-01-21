using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    string m_gameoverSe = "Game Over";

    //ゲームオーバー時のコールバック
    public delegate void GameOverEvent();
    GameOverEvent gameOverCallback;
    public event GameOverEvent AddGameOverEvent
    {
        add { gameOverCallback += value; }
        remove { gameOverCallback -= value; }
    }

    GameOverWindwCon m_gameOverCon;

    private void Awake()
    {
        GetGameOverEvents();
        m_gameOverCon = FindObjectOfType<GameOverWindwCon>();
    }

    private void Start()
    {
        LevelManager.Instance.GameOverManager = this;
    }

    /// <summary> ゲームオーバー時の処理をEventに登録 </summary>
    private void GetGameOverEvents()
    {
        IGameoverEvent[] gameoverEvents = GameObjectExtensions.FindObjectsOfInterface<IGameoverEvent>();
        foreach (var item in gameoverEvents)
        {
            AddGameOverEvent += item.GameOver;
        }
    }

    /// <summary> ゲームオーバー </summary>
    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        gameOverCallback?.Invoke();

        AudioManager.Instance.PlaySE(m_gameoverSe);

        //StartCoroutine(FindObjectOfType<MainUICon>().MainUIFadeOut());

        yield return null;

        m_gameOverCon?.GameOverWindowOpne();
    }
}
