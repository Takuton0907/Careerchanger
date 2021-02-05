
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    //次のステージのオブジェクトを設定
    [SerializeField, Header("ここにあるステージがインスタンスされます")]
    private StageData m_nextStage = null;

    [Header("全ステージのデータを管理")]
    public StageData[] stageDatas = new StageData[1];

    [SerializeField, Header("ステージごとのスコアを管理しています")]
    private ScoreRank scoreRank = null;
    
    [SerializeField, Header("コンボデータの管理をしています")] 
    ComboData[] m_comboDatas = null;

    /// <summary> 次のステージのスコアをセットします </summary>
    public void ScoreRankSet(ScoreRank scoreRankData)
    {
        scoreRank = scoreRankData;
    }
    /// <summary> 次のステージのスコアを返します </summary>
    public ScoreRank GetScoreRank()
    {
        return scoreRank;
    }

    /// <summary> 次のステージデータをセットします </summary>
    public void StageSet(StageData stage)
    {
        m_nextStage = stage;
    }
    /// <summary> 次のステージデータを返します </summary>
    public StageData GetStage()
    {
        return m_nextStage;
    }

    /// <summary> コンボのデータを返します </summary>
    public ComboData GetComboData(AttackMode mode)
    {
        foreach (var comboData in m_comboDatas)
        {
            if (comboData.GetAttackMode() == mode)
            {
                return comboData;
            }
        }
        return null;
    }

    /// <summary> Stageが現在遊べる状態なのかを返します </summary>
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
