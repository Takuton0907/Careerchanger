using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    /// <summary> デバッグモード </summary>
    public bool DebugMode = true;
    /// <summary>フェード中の透明度</summary>
    private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    public bool isFading { private set; get; } = false;
    /// <summary>フェード色</summary>
    public Color fadeColor = Color.black;
    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    /// <summary> フェードさせる際のUI </summary>
    [SerializeField, Header("背景Image")] Image fadeImage = null;

    [SerializeField, Header("キャラクターImage")] CanvasGroup UIs = null;

    [SerializeField, Header("キャラクターアニメーター")] Animator animator = null;

    [SerializeField, Header("強制的に止める時間")] float intervalTime = 1f; 

    public new void Awake()
    {
        base.Awake();
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        //DontDestroyOnLoad(this.gameObject);
    }

    //public void OnGUI()
    //{
    //    //// Fade .
    //    //if (this.isFading)
    //    //{
    //    //    //色と透明度を更新して白テクスチャを描画 .
    //    //    this.fadeColor.a = this.fadeAlpha;
    //    //    GUI.color = this.fadeColor;
    //    //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    //    //}

    //    if (this.DebugMode)
    //    {
    //        if (!this.isFading)
    //        {
    //            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
    //            myButtonStyle.fontSize = 40;

    //            GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
    //            myLabelStyle.fontSize = 40;

    //            GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
    //            myBoxStyle.fontSize = 40;

    //            List<string> scenes = new List<string>();
    //            scenes.Add("TITLE");
    //            scenes.Add("GAME");
    //            scenes.Add("SELECT");

    //            GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height / 6), "Fade Manager(Debug Mode)", myBoxStyle);
    //            GUI.Label(new Rect(20, 20, Screen.width - 20, Screen.height / 5), "Current Scene : " + SceneManager.GetActiveScene().name, myLabelStyle);

    //            for (int i = 0; i < scenes.Count; i++)
    //            {
    //                if (GUI.Button(new Rect(Screen.width / scenes.Count * i + 20, Screen.height / 5 - 120, 300, 50), $"Load {scenes[i]}", myButtonStyle))
    //                {
    //                    LoadScene(scenes[i], 1.0f);
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// 画面遷移 .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadScene(string scene, float interval)
    {
        if (isFading) return;
        AudioManager.Instance?.FadeOutBGM();
        StartCoroutine(TransScene(scene, interval));
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
        //背景フェードイン
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            fadeColor.a = fadeAlpha;
            fadeImage.color = fadeColor;
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return null;
        }

        //キャラクターフェードイン
        animator.enabled = true;
        animator.Play("anim");
        time = 0;
        while (time <= 0.5f)
        {
            UIs.alpha = fadeAlpha;
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }

        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(scene);
        time = 0;
        while (!async.isDone || time <= intervalTime)
        {
            yield return null;
            time += Time.deltaTime;
        }

        //キャラクターフェードアウト
        time = 0;
        while (time <= 0.5f)
        {
            UIs.alpha = fadeAlpha;
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        animator.enabled = false;

        //背景フェードアウト
        time = 0;
        fadeAlpha = fadeColor.a;
        while (time <= interval)
        {
            fadeColor.a = fadeAlpha;
            fadeImage.color = fadeColor;
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return null;
        }
        this.isFading = false;
    }
}

