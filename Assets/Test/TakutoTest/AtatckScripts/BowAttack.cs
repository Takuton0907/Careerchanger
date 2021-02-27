using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : AttackBase
{
    [SerializeField] float m_arrowAngle = 20f; //2本目の矢の角度
    [SerializeField] GameObject m_arrow = null;
    [SerializeField] GameObject m_angleArrow = null;
    protected override IEnumerator AttackCoroutine()
    {
        var angle = new Vector3(0, 0, m_arrowAngle);
        Instantiate(m_arrow, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(m_angleArrow, transform.position + new Vector3(1, 0, 0), Quaternion.Euler(angle));
        yield return null;
    }
}
