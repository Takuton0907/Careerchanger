using System.Collections;
using UnityEngine;

public class SelectSceneManager : SingletonMonoBehaviour<SelectSceneManager>
{
    PlaySelecter m_playSelecter = null;

    StageSceneButton m_stageSceneButton = null;

    [Header("再生するBGMの名前")]
    [SerializeField] string m_titleBgmName = "Map BGM";

    private new void Awake()
    {
        base.Awake();
        m_playSelecter = FindObjectOfType<PlaySelecter>();
        m_stageSceneButton = GetComponent<StageSceneButton>();
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(m_titleBgmName);
    }

    //次のレベルの準備・遷移
    public void LevelSelect(Stage stage)
    {
        m_playSelecter.SelecterFadeIn();
        m_stageSceneButton?.ScoerImageChange(stage);
        DataManager.Instance.LevelSelect(stage);
    }

    public void LoadLevel()
    {
        StartCoroutine(LevelLoad());
        AudioManager.Instance.PlaySE("sound_ok");
    }

    public void Cancel()
    {
        m_playSelecter.SelecterFadeOut();
    }

    IEnumerator LevelLoad()
    {
        yield return null;
        FadeManager.Instance.LoadScene("WeaponSelect", 2);
    }
}