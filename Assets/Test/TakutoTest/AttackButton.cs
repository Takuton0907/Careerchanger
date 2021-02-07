using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    //自分がシーン上でどのボタンなのかを決める
    [SerializeField] int m_selfAttackButtonNumaber = 0;
    //自分の攻撃方法を決める
    [SerializeField] AttackMode m_myAttackMode;

    bool m_nextCombo = false;
    AttackManager m_attackCon;

    private void Start()
    {
        AttackMode attack = FlagManager.GetAttackButtons().LastWeapon[m_selfAttackButtonNumaber];
        if (attack != AttackMode.None)
        {
            m_myAttackMode = attack;
        }
        m_attackCon = FindObjectOfType<AttackManager>();

    }

    public void OnClickAttack()
    {
        m_attackCon.Attack(m_myAttackMode, m_nextCombo);
    }

    public void ComboChanger(List<AttackMode> attackModes)
    {
        foreach (var item in attackModes)
        {
            if (item == m_myAttackMode)
            {
                m_nextCombo = true;
                Debug.Log($"{gameObject.name}の{m_myAttackMode}が次のコンボ対象です");
                return;
            }
            else
            {
                m_nextCombo = false;
            }
        }
        if(!m_nextCombo)
        {
            Debug.Log($"{gameObject.name}の({m_myAttackMode})は次のコンボ対象ではありません");
        }
    }
}
