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
    };

    [SerializeField] Transform m_parentObje; //武器のオブジェクトのインスタンスのおやオブジェクト
    [SerializeField] GameObject[] m_weapons; //各武器オブジェクト（見た目のみ）
    [SerializeField] GameObject m_arrow; //矢用のオブジェクト
    [SerializeField] GameObject m_angleArrow; //2本目の矢
    [SerializeField] float m_arrowAngle; //2本目の矢の角度
    [SerializeField] float m_smashSpeed = 1;

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
        m_weapons[weaponDic[attackMode]].SetActive(true);
        switch (attackMode)
        {
            case AttackMode.Sword:
                StartCoroutine(AttackSword());
                break;
            case AttackMode.Axe:
                StartCoroutine(AttackAxe());
                break;
            case AttackMode.Spear:
                StartCoroutine(AttackSpear());
                break;
            case AttackMode.Staff:
                StartCoroutine(AttackStaff());
                break;
            case AttackMode.Katana:
                StartCoroutine(AttackKatana());
                break;
            case AttackMode.Bow:
                AttackBow();
                break;
            case AttackMode.Smash:
                StartCoroutine(AttackSmash());
                break;
        }
        if (m_comboManager == null) return;
        List<AttackMode> attackModes = m_comboManager.Combo(attackMode, combo);
        if (attackModes != null) _attackEvent?.Invoke(attackModes);
    }
    /// <summary> 各武器の生成 </summary>
    private void WeaponsInstance()
    {
        Transform playerTrans = LevelManager.Instance.PlayerCon.transform;

        m_weaponCollider = new BoxCollider2D[m_weapons.Length];

        if (playerTrans != null)// プレイヤーの子オブジェクトにしたときにサイズが変わらないようにする
        {
            m_parentObje.localScale = new Vector3(m_parentObje.localScale.x / playerTrans.localScale.x, m_parentObje.localScale.y / playerTrans.localScale.y);
        }

        for (int i = 0; i < m_weapons.Length; i++)
        {
            if (playerTrans != null)
            {
                m_weapons[i] = Instantiate(m_weapons[i], m_parentObje);
                if (m_weapons[i].TryGetComponent(out TrrigerHitAttack trrigerHitAttack))
                {
                    trrigerHitAttack.SetParentObj(LevelManager.Instance.PlayerCon.transform);
                }
            }
            else
            {
                m_weapons[i] = Instantiate(m_weapons[i], transform);
            }
            m_weaponCollider[i] = m_weapons[i].GetComponent<BoxCollider2D>();
        }
    }

    //各武器の攻撃
    //------------------------------------------------------------------------------
    IEnumerator AttackSword()
    {
        if (m_weaponCollider[weaponDic[AttackMode.Sword]])
        {
            m_weaponCollider[weaponDic[AttackMode.Sword]].enabled = true;
            yield return new WaitForSeconds(0.2f);
            m_weaponCollider[weaponDic[AttackMode.Sword]].enabled = false;
        }
    }
    IEnumerator AttackAxe()
    {
        if (m_weaponCollider[weaponDic[AttackMode.Axe]])
        {
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = false;
        }
    }
    IEnumerator AttackSpear()
    {
        if (m_weaponCollider[weaponDic[AttackMode.Spear]])
        {
            m_weaponCollider[weaponDic[AttackMode.Spear]].enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider[weaponDic[AttackMode.Spear]].enabled = false;
        }
    }
    IEnumerator AttackStaff()
    {
        if (m_weaponCollider[weaponDic[AttackMode.Axe]])
        {
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = true;
            yield return new WaitForSeconds(1f);
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = false;
        }
    }
    IEnumerator AttackKatana()
    {
        if (m_weaponCollider[weaponDic[AttackMode.Axe]])
        {
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider[weaponDic[AttackMode.Axe]].enabled = false;
        }
    }
    IEnumerator AttackSmash()
    {
        float colMaxSizeX = m_weaponCollider[weaponDic[AttackMode.Smash]].size.x;
        m_weaponCollider[weaponDic[AttackMode.Smash]].size = new Vector2(0, m_weaponCollider[weaponDic[AttackMode.Smash]].size.y);
        if (m_weaponCollider[weaponDic[AttackMode.Smash]])
        {
            m_weaponCollider[weaponDic[AttackMode.Smash]].enabled = true;
            while (m_weaponCollider[weaponDic[AttackMode.Smash]].size.x <= colMaxSizeX)
            {
                m_weaponCollider[weaponDic[AttackMode.Smash]].size += new Vector2(Time.deltaTime * m_smashSpeed, 0);
                yield return null;
            }
            m_weaponCollider[weaponDic[AttackMode.Smash]].enabled = true;
        }
    }
    private void AttackBow()
    {
        var angle = new Vector3(0, 0, m_arrowAngle);
        Instantiate(m_arrow, m_weapons[weaponDic[AttackMode.Bow]].transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(m_angleArrow, m_weapons[weaponDic[AttackMode.Bow]].transform.position + new Vector3(1, 0, 0), Quaternion.Euler(angle));
    }
    //------------------------------------------------------------------------------
    /// <summary> コンボ時に呼び出す関数を登録する </summary>
    private void GetAttackEvents()
    {
        AttackButton[] attackButtons = FindObjectsOfType<AttackButton>();
        foreach (var item in attackButtons)
        {
            _attackEvent += item.ComboChanger;
        }
    }
}