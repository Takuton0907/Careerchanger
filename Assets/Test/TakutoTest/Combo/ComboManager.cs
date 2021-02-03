using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] List<Combo> m_combos;
}

[Serializable]
public class Combo
{
    public List<AttackMode> combos = new List<AttackMode>();
}