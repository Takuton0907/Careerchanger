using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] GameObject StageStartOpsion;

    public GameObject[] dummyStage = new GameObject[0];

    public void StageOpenJudge()
    {
        for (int i = 1; i < 6; i++)
        {
            bool judge = FlagManager.Get(Stage.Stage1 + i).Clear;
            if (judge)
            {
                stageOpen(i);
            }
        }
        stageOpen(0);
    }

    public void stageOpen(int[] dummyNumber)
    {
        foreach (var item in dummyNumber)
        {
            dummyStage[item].SetActive(false);
        }
    }

    public void stageOpen(int dummyNumber)
    {
       dummyStage[dummyNumber].SetActive(false);  
    }

    public void OpenSelectWindow()
    {
        StageStartOpsion.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        StageStartOpsion.SetActive(false);
    }
}
