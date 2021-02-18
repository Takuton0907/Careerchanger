using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmashAttack : AttackBase
{
    [SerializeField] float m_smashSpeed = 100;

    [SerializeField] new BoxCollider2D m_collider;

    protected override void Start()
    {
        base.Start();
    }

    protected override IEnumerator AttackCoroutine()
    {
        if (m_collider != null)
        {
            float colMaxSizeX = m_collider.size.x;
            float colOffsetX = m_collider.offset.x;
            m_collider.size = new Vector2(0, m_collider.size.y);
            m_collider.enabled = true;

            float timer = 0;
            float time = 2;

            while (timer < time)
            {
                m_collider.size = Vector2.Lerp(new Vector2(0, m_collider.size.y), new Vector2(colMaxSizeX, m_collider.size.y), timer / time);
                m_collider.offset = Vector2.Lerp(new Vector2(0, m_collider.offset.y), new Vector2(colOffsetX, m_collider.offset.y), timer / time);
                timer += Time.deltaTime;
                yield return null;
            }
            m_collider.offset = new Vector2(colOffsetX, m_collider.offset.y);
            m_collider.enabled = false;
        }
        gameObject.SetActive(false);
    }
}
