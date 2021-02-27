using System;
using System.Collections.Generic;
using UnityEngine;

public class TrrigerHitAttack : MonoBehaviour
{
    [SerializeField] int m_attackPower = 1;
    /// <summary> 攻撃する親のオブジェクトを設定　空欄であれば全員に当たる </summary>
    [SerializeField] Transform m_parentObj = null;

    public delegate void AttackFunc();
    private AttackFunc m_fanc { get; set; }

    public void SetParentObj(Transform parent)
    {
        m_parentObj = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageHandler damage))
        {
            if (collision.transform == m_parentObj) return;
            damage.Damage(-m_attackPower);
            m_fanc?.Invoke();
            //Debug.Log($"{transform.name} が {collision.transform.name} に {m_attackPower} 与えた" +
            //    $"\nPlayerの残りライフは {LevelManager.Instance.LifeManager.GetCurrentLife}");
        }
    }

    public void SetAttackFunc(AttackFunc func)
    {
        m_fanc += func;
    }
}
