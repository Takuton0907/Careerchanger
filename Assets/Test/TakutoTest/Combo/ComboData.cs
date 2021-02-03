using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData", menuName = "ScriptableObjects/ComboData")]
public class ComboData : ScriptableObject
{
    [SerializeField]
    List<Combo> m_comboData = new List<Combo>();
}
