using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanager : MonoBehaviour
{
    [SerializeField]
    SceneNames m_sceneNames = SceneNames.Title;
    [SerializeField]
    float fadetime = 2;
    [SerializeField]
    string m_tochSe = "sound_ok";

    public void SceneLoad()
    {
        FadeManager.Instance.LoadScene(m_sceneNames.ToString(), fadetime);
        AudioManager.Instance.PlaySE(m_tochSe);
    }
}

enum SceneNames
{
    Title,
    StageSelect,
    WeaponSelect,
    Game,
}