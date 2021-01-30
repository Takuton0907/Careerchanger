using System;
using System.Collections.Generic;
using UnityEngine;

public class ConboManager : MonoBehaviour
{
    [SerializeField] List<Combo> m_combos;
}

[Serializable]
class Combo
{
    public string name;
    public AttackMode[] combos = new AttackMode[3];
}