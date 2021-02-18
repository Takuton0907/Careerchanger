using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : AttackBase
{
    [SerializeField] float m_smashSpeed = 100;

    [SerializeField] new BoxCollider2D m_collider;

    protected override IEnumerator AttackCoroutine()
    {
        if (m_collider != null)
        {
            float colMaxSizeX = m_collider.size.x;
            float colOffsetX = m_collider.offset.x;
            m_collider.size = new Vector2(0, m_collider.size.y);
            m_collider.enabled = true;
            while (m_collider.size.x < colMaxSizeX)
            {
                m_collider.size += new Vector2(Time.deltaTime * m_smashSpeed, 0);
                m_collider.offset += new Vector2(Time.deltaTime * m_smashSpeed, 0);
                yield return null;
            }
            m_collider.offset = new Vector2(colOffsetX, m_collider.offset.y);
            m_collider.enabled = false;
        }
    }
}
