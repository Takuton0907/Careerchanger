using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    /// <summary> 今現在管理しているコンボを持つ </summary>
    private List<Combo> m_combos;
    /// <summary> 現在のコンボ数（特定の武器のつながり）をカウントします </summary>
    private int m_comboCounter = 0;

    private void Awake()
    {
        LevelManager.Instance.ComboManager = this;
    }

    /// <summary> コンボのチェック </summary>
    public List<AttackMode> ComboChack(AttackMode attackMode)
    {
        var nextAttackModes = new List<AttackMode>();

        m_comboCounter++;

        if (m_combos == null)
        {
            m_combos = GetNextCombo(attackMode);
            for (int i = 0; i < m_combos.Count; i++)
            {
                nextAttackModes.Add(m_combos[i].combos[m_comboCounter]);
            }

            return nextAttackModes;
        }
        else
        {
            for (int i = 0; i < m_combos.Count; i++)
            {
                nextAttackModes.Add(m_combos[i].combos[m_comboCounter]);
            }

            return nextAttackModes;
        }
    }

    /// <summary> 次のコンボデータの配列をGetします </summary>
    private List<Combo> GetNextCombo(AttackMode attackMode) => DataManager.Instance.GetComboData(attackMode).GetCombos();
}
