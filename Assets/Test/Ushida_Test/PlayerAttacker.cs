using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttacker : MonoBehaviour
{
    Dictionary<AttackMode, int> weaponDic = new Dictionary<AttackMode, int>() { //ディクショナリに各Enumに適応した数値を入れておく（武器オブジェクトの参照用）
        {AttackMode.Sword, 0 },
        {AttackMode.Axe, 1 },
        {AttackMode.Spear, 2 },
        {AttackMode.Staff, 3 },
        {AttackMode.Katana, 4 },
        {AttackMode.Bow, 5 },
        {AttackMode.Chain, 6 }
    };

    [SerializeField] GameObject[] m_weapons; //各武器オブジェクト（見た目のみ）
    [SerializeField] GameObject m_arrow; //矢用のオブジェクト
    [SerializeField] GameObject m_angleArrow; //2本目の矢
    [SerializeField] float m_arrowAngle; //2本目の矢の角度

    BoxCollider2D m_weaponCollider;
    AttackMode m_currentMode;

    public enum AttackMode
    {
        Sword,
        Axe,
        Spear,
        Staff,
        Katana,
        Bow,
        Chain,
        Count,
    }

    void Start()
    {
        m_currentMode = AttackMode.Sword;
        m_weapons[weaponDic[m_currentMode]].SetActive(true);
        m_weaponCollider = m_weapons[weaponDic[m_currentMode]].GetComponent<BoxCollider2D>();
        m_weaponCollider.enabled = false;

        Debug.Log(m_currentMode.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (m_currentMode)
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
                case AttackMode.Chain:
                    AttackChain();
                    break;
            }
        }

        if (Input.GetButtonDown("Fire2")) // 武器チェンジ
        {
            //いったんすべての武器を非表示にする
            foreach (var weapon in m_weapons)
            {
                weapon.SetActive(false);
            }

            m_currentMode++; // 武器をチェンジする
            if (m_currentMode == AttackMode.Count)
            {
                m_currentMode = AttackMode.Sword;
            }

            m_weapons[weaponDic[m_currentMode]].SetActive(true); //チェンジした武器を表示する
            m_weaponCollider = m_weapons[weaponDic[m_currentMode]].GetComponent<BoxCollider2D>(); //コライダーをチェンジする
            if (m_weaponCollider)
            {
                m_weaponCollider.enabled = false;
            }
            Debug.Log(m_currentMode.ToString());
        }
    }

    IEnumerator AttackSword()
    {
        if (m_weaponCollider)
        {
            m_weaponCollider.enabled = true;
            yield return new WaitForSeconds(0.2f);
            m_weaponCollider.enabled = false;
        }
    }

    IEnumerator AttackAxe()
    {
        if (m_weaponCollider)
        {
            m_weaponCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider.enabled = false;
        }
    }

    IEnumerator AttackSpear()
    {
        if (m_weaponCollider)
        {
            m_weaponCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider.enabled = false;
        }
    }

    IEnumerator AttackStaff()
    {
        if (m_weaponCollider)
        {
            m_weaponCollider.enabled = true;
            yield return new WaitForSeconds(1f);
            m_weaponCollider.enabled = false;
        }
    }

    IEnumerator AttackKatana()
    {
        if (m_weaponCollider)
        {
            m_weaponCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_weaponCollider.enabled = false;
        }
    }

    void AttackBow()
    {
        var angle = new Vector3(0, 0, m_arrowAngle);
        Instantiate(m_arrow, m_weapons[weaponDic[m_currentMode]].transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(m_angleArrow, m_weapons[weaponDic[m_currentMode]].transform.position + new Vector3(1, 0, 0), Quaternion.Euler(angle));
    }

    void AttackChain()
    {

    }
}
