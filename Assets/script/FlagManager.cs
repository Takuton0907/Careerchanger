using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    Stage1,
    Stage2,
    Stage3,
    Stage1_EX,
    Stage2_EX,
    Stage3_EX,
}

public enum AttackMode
{
    None,
    Sword,
    Axe,
    Spear,
    Staff,
    Katana,
    Bow,
}

public class Flags
{
    public bool Clear = false;
    public uint Rating = 0;
    public uint EnemyDes = 0;
    public uint Combo = 0;
    public uint MaxScoer = 0;
    public AttackMode[] LastWeapon = new AttackMode[3];
}

public class FlagManager
{
    public static Dictionary<Stage, Flags> Flag = new Dictionary<Stage, Flags>();

    [RuntimeInitializeOnLoadMethod]
    static void GameStart()
    {
        for (int i = 0; i < 6; i++)
        {
            Flag.Add(Stage.Stage1 + i, new Flags());
        }
    }

    public static Flags Get(Stage key)
    {
        return Flag[key];
    }

    public static Flags GetAttackButtons()
    {
        StageData stageData = DataManager.Instance.GetStage();
        return Get(stageData.stageNum);
    }
}