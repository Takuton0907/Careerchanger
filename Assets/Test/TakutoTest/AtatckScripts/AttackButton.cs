using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    //自分がシーン上でどのボタンなのかを決める
    [SerializeField] int m_selfAttackButtonNumaber = 0;
    //自分の攻撃方法を決める
    [SerializeField] protected AttackMode m_myAttackMode;

    [SerializeField] float m_recastTime = 5;

    bool m_nextCombo = false;
    protected AttackManager m_attackCon;
    protected Button m_button;
    protected Text m_recastText;

    protected virtual void Start()
    {
        m_attackCon = FindObjectOfType<AttackManager>();
        if (m_selfAttackButtonNumaber >= 3) return;
        AttackMode attack = FlagManager.GetAttackButtons().LastWeapon[m_selfAttackButtonNumaber];
        if (attack != AttackMode.None)
        {
            m_myAttackMode = attack;
        }
        m_button = GetComponent<Button>();
        m_recastText = GetComponentInChildren<Text>();
        Able();
    }

    public void OnClickAttack()
    {
        m_attackCon?.Attack(m_myAttackMode, m_nextCombo);
        Unable();
    }

    public void ComboChanger(List<AttackMode> attackModes)
    {
        foreach (var item in attackModes)
        {
            if (item == m_myAttackMode)
            {
                m_nextCombo = true;
                Debug.Log($"{gameObject.name}の{m_myAttackMode}が次のコンボ対象です");
                return;
            }
            else
            {
                m_nextCombo = false;
            }
        }
        if(!m_nextCombo)
        {
            Debug.Log($"{gameObject.name}の({m_myAttackMode})は次のコンボ対象ではありません");
        }
    }

    public virtual void Able()
    {
        m_button.interactable = true;
        m_recastText?.gameObject.SetActive(false);
    }

    public virtual void Unable()
    {
        m_button.interactable = false;
        StartCoroutine(Recast());
        m_recastText?.gameObject.SetActive(true);
    }

    protected virtual IEnumerator Recast()
    {
        float timer = m_recastTime;
        while (0 < timer)
        {
            m_recastText.text = timer.ToString("0");
            //リキャストアニメーション
            timer -= Time.deltaTime;
            yield return null;
        }
        Able();
    }

    public void SetRevastTime(float time) => m_recastTime = time;

    public AttackMode GetAttackMode() => m_myAttackMode;
}