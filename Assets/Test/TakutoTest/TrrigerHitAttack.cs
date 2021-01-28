using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrrigerHitAttack : MonoBehaviour
{
    [SerializeField] int m_attackPower = 1;

    /// <summary> 攻撃する親のオブジェクトを設定　空欄であれば全員に当たる </summary>
    [SerializeField] Transform m_parentObj = null;

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
            Debug.Log($"{transform.name} が {collision.transform.name} に {m_attackPower} 与えた" +
                $"\nPlayerの残りライフは {LevelManager.Instance.LifeManager.GetCurrentLife}");
        }
    }
}
