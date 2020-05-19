using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Canvas canvas;
    private static Image image;

    // シングルトン
    private static Fade singlton;
    public static Fade Instance
    {
        get
        {
            if (singlton == null)
            {
                Init();
            }
            return singlton;
        }
    }
    private Fade() { }
    private static void Init()
    {
        // Canvas作成
        GameObject canvasObject = new GameObject("CanvasFade");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        // Image作成
        image = new GameObject("ImageFade").AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);
        // 画面中央をアンカーとし、Imageのサイズをスクリーンサイズに合わせる。
        image.rectTransform.anchoredPosition = Vector3.zero;
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // 遷移先シーンでもオブジェクトを破棄しない。
        DontDestroyOnLoad(canvas.gameObject);

        // シングルトンオブジェクトを保持する。
        // newの場合、オブジェクトが正しく初期化されない可能性があるので、
        // Add - GetComponentを通して、オブジェクトを取得する。
        canvasObject.AddComponent<Fade>();
        singlton = canvasObject.GetComponent<Fade>();
    }

    // 画面遷移を行う。
    // interval - フェードイン，フェードアウトに要する時間(秒)
    public void LoadScene(float interval, string sceneName)
    {
        StartCoroutine(FadeStart(interval, sceneName));
    }

    private IEnumerator FadeStart(float interval, string sceneName)
    {
        float time = 0f;
        canvas.enabled = true;

        // フェードイン
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // シーン非同期ロード
        yield return SceneManager.LoadSceneAsync(sceneName);

        // フェードアウト
        time = 0f;
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        // 描画を更新しない。
        canvas.enabled = false;
    }
}