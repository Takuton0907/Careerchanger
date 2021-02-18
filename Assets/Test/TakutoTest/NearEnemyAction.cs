using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemyAction : EnemyBase
{
    [SerializeField] GameObject m_attackObj = null;

    bool m_attackFlag = false;

    protected override void Move()
    {
    }

    protected override void Start()
    {
        base.Start();
        m_attackObj.SetActive(false);
    }

    IEnumerator AttackCol()
    {
        yield return new WaitForSeconds(1.5f);
        m_attackObj.SetActive(true);

        yield return new WaitForSeconds(1);
        m_attackObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (m_attackFlag) return;
            Attack();
            StartCoroutine(AttackCol());
            m_attackFlag = true;
        }
    }
}
