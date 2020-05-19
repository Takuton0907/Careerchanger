using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneButton : MonoBehaviour
{
    StageSelectManager selectManager;

    [SerializeField] Text challengeText;
    [SerializeField] string stageName = "ステージ";
    [SerializeField] string EXstageName = "隠しステージ";
    [SerializeField] string challenge = "に挑戦しますか？";
    [SerializeField] Image ScoerImage = null;
    [SerializeField] float fadeInterval = 1;
    
    public static int stage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        selectManager = GetComponent<StageSelectManager>();
        selectManager.StageOpenJudge();
    }

    public void OnClickBack()
    {
        Fade.Instance.LoadScene(fadeInterval, "Title");
    }

    public void OnClickOpenWindow()
    {
        selectManager.OpenSelectWindow();
        if (stage < 4)
        {
            challengeText.fontSize = 30;
            challengeText.text = stageName + stage.ToString() + challenge;
        }
        else
        {
            challengeText.fontSize = 25;
            challengeText.text = EXstageName + (stage - 3).ToString() + challenge;
        }
        ScoerImageChange(Stages.Stage1 + stage);
    }

    public void OnClickCloseWindow()
    {
        selectManager.CloseSelectWindow();
        stage = 0;
    }

    public void stageSelect(int stageNumber)
    {
        stage = stageNumber;
        OnClickOpenWindow();
    }

    public void OnClickWeaponSelect()
    {
        Fade.Instance.LoadScene(fadeInterval, "WeaponSelect");
    }

    private void ScoerImageChange(Stages index)
    {
        uint scoer = FlagManager.Flag[index].MaxScoer;
        string ImagePath = string.Empty;
        if (scoer >= 10000)
        {
            ImagePath = "RankA";
        }
        else if (scoer >= 5000)
        {
            ImagePath = "RankB";
        }
        else if (scoer >= 2500)
        {
            ImagePath = "RankC";
        }
        else
        {
            ImagePath = "NoRank";
        }

        try
        {
            ScoerImage.sprite = Resources.Load(ImagePath) as Sprite;
        }
        catch (System.Exception)
        {

        }
    }
}
