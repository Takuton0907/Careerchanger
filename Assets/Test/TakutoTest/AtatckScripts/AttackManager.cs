using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    Dictionary<AttackMode, int> weaponDic = new Dictionary<AttackMode, int>() { //ディクショナリに各Enumに適応した数値を入れておく（武器オブジェクトの参照用）
        {AttackMode.Sword, 0 },
        {AttackMode.Axe, 1 },
        {AttackMode.Spear, 2 },
        {AttackMode.Staff, 3 },
        {AttackMode.Katana, 4 },
        {AttackMode.Bow, 5 },
        {AttackMode.Smash, 6 },
    };

    [SerializeField] Transform m_parentObje; //武器のオブジェクトのインスタンスのおやオブジェクト
    [SerializeField] AttackBase[] m_attacks;

    BoxCollider2D[] m_weaponCollider;//各武器のコライダーの参照配列
    ComboManager m_comboManager;

    delegate void AttackEvent(List<AttackMode> attackModes);
    event AttackEvent _attackEvent;

    void Start()
    {
        WeaponsInstance();
        GetAttackEvents();
        m_comboManager = LevelManager.Instance.ComboManager;
    }

    /// <summary> 攻撃 </summary>
    public void Attack(AttackMode attackMode, bool combo)
    {
        int type = weaponDic[attackMode];
        m_attacks[type].gameObject.SetActive(true);

        m_attacks[type].Attack();

        if (m_comboManager == null) return;
        List<AttackMode> attackModes = m_comboManager.Combo(attackMode, combo);
        if (attackModes != null) _attackEvent?.Invoke(attackModes);
    }
    /// <summary> 各武器の生成 </summary>
    private void WeaponsInstance()
    {
        Transform playerTrans = LevelManager.Instance.PlayerCon.transform;

        m_weaponCollider = new BoxCollider2D[m_attacks.Length];

        if (playerTrans != null)// プレイヤーの子オブジェクトにしたときにサイズが変わらないようにする
        {
            m_parentObje.localScale = new Vector3(m_parentObje.localScale.x / playerTrans.localScale.x, m_parentObje.localScale.y / playerTrans.localScale.y);
        }

        for (int i = 0; i < m_attacks.Length; i++)
        {
            if (playerTrans != null)
            {
                m_attacks[i] = Instantiate(m_attacks[i], m_parentObje);
                if (m_attacks[i].TryGetComponent(out TrrigerHitAttack trrigerHitAttack))
                {
                    trrigerHitAttack.SetParentObj(LevelManager.Instance.PlayerCon.transform);
                }
            }
            else
            {
                m_attacks[i] = Instantiate(m_attacks[i], transform);
            }
            m_weaponCollider[i] = m_attacks[i].GetComponent<BoxCollider2D>();
        }
    }

    private void GetAttackEvents()
    {
        AttackButton[] attackButtons = FindObjectsOfType<AttackButton>();
        foreach (var item in attackButtons)
        {
            _attackEvent += item.ComboChanger;
        }

        foreach (var item in attackButtons)
        {
            item.SetRevastTime(m_attacks[weaponDic[item.GetAttackMode()]].GetRecastTime());
        }
    }
}