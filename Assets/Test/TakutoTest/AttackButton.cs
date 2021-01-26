using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    [SerializeField] int m_selfAttackButtonNumaber = 0;

    AttackMode m_myAttackMode = AttackMode.Sword;
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
        m_attackCon.Attack(m_myAttackMode);
    }
}
