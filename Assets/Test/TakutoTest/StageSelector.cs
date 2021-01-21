using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageSelector", order = 1)]
public class StageSelector : ScriptableObject
{
    [Header("Size = 全部のステージ数")]
    public StageData[] stageDatas = new StageData[1];
}

[Serializable]
public class StageData
{
    [Header("自分のステージ番号")]
    public Stage stageNum;
    [Header("ステージのprefab")]
    public GameObject stage;
    [Header("スコアのランク分け")]
    public ScoreRank scoreRankDate;
    [Header("BGMの名前")]
    public string stageBgmName;
}