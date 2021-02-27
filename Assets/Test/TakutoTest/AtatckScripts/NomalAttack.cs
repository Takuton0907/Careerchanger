using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttack : AttackBase
{
    [SerializeField] float m_attackTime = 0.5f;

    protected override IEnumerator AttackCoroutine()
    {
        if (m_collider != null)
        {
            float timer = 0;
            m_collider.enabled = true;
            while (timer < m_attackTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            m_collider.enabled = false;
        }
    }
}
