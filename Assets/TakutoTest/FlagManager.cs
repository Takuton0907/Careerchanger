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

public class Flags
{
    public bool Clear = false;
    public uint Rat = 0;
    public uint EnemyDes = 0;
    public uint Combo = 0;
    public uint LastWeapon = 0;
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
}
