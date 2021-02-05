using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData", menuName = "ScriptableObjects/ComboData"), Serializable]
public class ComboData : ScriptableObject
{
    [SerializeField]
    AttackMode m_mode = AttackMode.None;
    [SerializeField]
    List<Combo> m_comboData = new List<Combo>();

    public List<Combo> GetCombos() => m_comboData;
    public AttackMode GetAttackMode() => m_mode;
}
