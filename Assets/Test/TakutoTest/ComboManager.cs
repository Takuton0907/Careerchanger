using System;
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

    /// <summary> コンボ </summary>
    public List<AttackMode> Combo(AttackMode attackMode, bool combo)
    {
        if (!combo && m_combos != null)//間違ったコンボの時の判定
        {
            m_combos = null;
            m_comboCounter = 0;
            Debug.Log($"{attackMode}でコンボ失敗");
            return null;
        } 

        var nextAttackModes = new List<AttackMode>();
        m_comboCounter++;
        if (m_combos == null)//コンボのデータがなければ追加新たに取得
        {
            ComboData comboData = DataManager.Instance.GetComboData(attackMode);
            if (comboData == null)
            {
                Debug.Log($"{attackMode}から始まるコンボが見つかりませんでした");
                m_comboCounter = 0;
                return null;
            }
            m_combos = comboData.GetCombos();
            for (int i = 0; i < m_combos.Count; i++)
            {
                nextAttackModes.Add(m_combos[i].combos[m_comboCounter]);
            }
            return nextAttackModes;
        }

        for (int i = 0; i < m_combos.Count; i++)
        {
            if (m_combos[i].combos.Count == m_comboCounter)
            {
                if (attackMode == m_combos[i].combos[m_comboCounter - 1])
                {
                    m_comboCounter = 0;
                    m_combos = null;
                    Debug.Log("コンボ完了！");
                    return null;
                    //コンボが最後まで行った
                }
            }
            nextAttackModes.Add(m_combos[i].combos[m_comboCounter]);
        }
        return nextAttackModes;
    }
}
