using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSceneButton : MonoBehaviour
{
    StageSelectManager selectManager;

    [SerializeField] Text challengeText;
    [SerializeField] string stageName = "ステージ";
    [SerializeField] string EXstageName = "隠しステージ";
    [SerializeField] string challenge = "に挑戦しますか？";
    [SerializeField] Image ScoerImage = null;
    
    static int stage = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        selectManager = GetComponent<StageSelectManager>();
        selectManager.StageOpenJudge();
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Title");
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
        ScoerImageChange(Stage.Stage1 + stage);
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
        SceneManager.LoadScene("WeaponSelect")
;    }

    public void OnclickPlay()
    {
        switch (stage)
        {
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
            case 4:
                SceneManager.LoadScene("Stage1_");
                break;
            case 5:
                SceneManager.LoadScene("Stage2_");
                break;
            case 6:
                SceneManager.LoadScene("Stage3_");
                break;
            default:
                Debug.Log("不適切な数値が代入されました");
                break;
        }
    }

    private void ScoerImageChange(Stage index)
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
