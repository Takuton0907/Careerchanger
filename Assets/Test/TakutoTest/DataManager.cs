
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    //次のステージのオブジェクトを設定
    [SerializeField]
    private StageData m_nextStage = null;

    [SerializeField]
    [Header("Size = 全部のステージ数")]
    public StageData[] stageDatas = new StageData[1];

    [SerializeField]
    private ScoreRank scoreRank = null;

    public bool FirstPlay { get; private set; }

    public void ScoreRankSet(ScoreRank scoreRankData)
    {
        scoreRank = scoreRankData;
    }

    public ScoreRank GetScoreRank()
    {
        return scoreRank;
    }

    public void StageSet(StageData stage)
    {
        m_nextStage = stage;
    }
    public StageData GetStage()
    {
        return m_nextStage;
    }

    public bool OpneStageCheck(Stage stage)
    {
        for (int i = 0; i < (int)stage; i++)
        {
            if (i + 1 >= stageDatas.Length)
            {
                Debug.LogError($"{stage}は存在しません");
                break;
            }
            if (!FlagManager.Flag[stage].Clear) return false;
        }
        return true;
    }

    //次のレベルの準備・遷移
    public void LevelSelect(Stage stage)
    {
        StageSet(stageDatas[(int)stage]);
    }

    public void StageClear()
    {
        for (int i = 0; i < FlagManager.Flag.Count; i++)
        {
            if (m_nextStage.stageNum == stageDatas[i].stageNum)
            {
                FlagManager.Flag[Stage.Stage1 + i].Clear = true;
                break;
            }
        }
    }
}
