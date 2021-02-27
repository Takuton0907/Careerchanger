using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneButton : MonoBehaviour
{
    [SerializeField] Image challengeImage;
    [SerializeField] Sprite[] challenges;
    [SerializeField] Image ScoerImage = null;
    [SerializeField] Sprite[] scoerImages;
    [SerializeField] float fadeInterval = 1;

    public void ScoerImageChange(Stage index)
    {
        ScoerImage.color = Color.white;
        uint scoer = FlagManager.Flag[index].MaxScoer;
        if (scoer >= 10000)
        {
            ScoerImage.sprite = scoerImages[2];
        }
        else if (scoer >= 5000)
        {
            ScoerImage.sprite = scoerImages[1];
        }
        else if (scoer >= 2500)
        {
            ScoerImage.sprite = scoerImages[0];
        }
        else
        {
            ScoerImage.sprite = null;
            ScoerImage.color = Color.clear;
        }
    }
}
