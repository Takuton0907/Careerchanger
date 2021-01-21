using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneButton : MonoBehaviour
{
    [SerializeField] Text challengeText;
    [SerializeField] string stageName = "ステージ";
    [SerializeField] string EXstageName = "隠しステージ";
    [SerializeField] string challenge = "に挑戦しますか？";
    [SerializeField] Image ScoerImage = null;
    [SerializeField] float fadeInterval = 1;

    private void OnEnable()
    {

    }

    private void ScoerImageChange(Stage index)
    {
        uint scoer = FlagManager.Flag[index].MaxScoer;
        string imagePath = string.Empty;
        if (scoer >= 10000)
        {
            imagePath = "RankA";
        }
        else if (scoer >= 5000)
        {
            imagePath = "RankB";
        }
        else if (scoer >= 2500)
        {
            imagePath = "RankC";
        }
        else
        {
            imagePath = "NoRank";
        }

        Sprite nextSprite = Resources.Load(imagePath) as Sprite;
        Debug.Log($"取得したスプライは {nextSprite}");
        if (nextSprite != null)
        {
            ScoerImage.sprite = nextSprite;
        }
    }
}
