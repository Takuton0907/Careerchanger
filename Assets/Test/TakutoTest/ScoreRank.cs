using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScoreRankData", fileName = "ScoreRankData")]
public class ScoreRank : ScriptableObject
{
    public int S_Rank;
    public int A_Rank;
    public int B_Rank;
    public int C_Rank;
    public int NoDamageBonus;
    public int AllEnmeyDefeatBonus;
}

public enum StageClearRank
{
    S,
    A,
    B,
    C,
}