using System.Collections;
using UnityEngine;

public class SelectSceneManager : SingletonMonoBehaviour<SelectSceneManager>
{
    [SerializeField]
    StageSelector m_stageSelector = null;

    PlaySelecter m_playSelecter = null;

    [Header("再生するBGMの名前")]
    [SerializeField] string m_titleBgmName = "Map BGM";

    private new void Awake()
    {
        base.Awake();
        //AudioManager.Instance.PlayBGM(m_bgmAudio.name);
        m_playSelecter = FindObjectOfType<PlaySelecter>();
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(m_titleBgmName);
    }

    //次のレベルの準備・遷移
    public void LevelSelect(Stage stage)
    {
        m_playSelecter.SelecterFadeIn();
        AudioManager.Instance.PlaySE("tap");
        DataManager.Instance.LevelSelect(stage);
    }

    public void LoadLevel()
    {
        StartCoroutine(LevelLoad());
        AudioManager.Instance.PlaySE("tap");
    }

    public void Cancel()
    {
        m_playSelecter.SelecterFadeOut();
        AudioManager.Instance.PlaySE("tap");
    }

    //遷移時に何かしらの演出がしたいと思ったのでとりあえず用意
    IEnumerator LevelLoad()
    {
        yield return null;
        FadeManager.Instance.LoadScene("Game", 2);
    }
}