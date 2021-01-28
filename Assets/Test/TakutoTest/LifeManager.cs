using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] Transform m_lifeParentTransform = null;
    [SerializeField] GameObject m_lifeUIPrefab = null;
    [SerializeField] int m_maxLife = 6;
    [SerializeField] Vector3 m_lifeUiSize = Vector3.one;
    public int GetMaxLife { get { return m_maxLife; } }
    int m_currentlife;

    private void Awake()
    {
        LevelManager.Instance.LifeManager = this;

        m_currentlife = m_maxLife;
    }

    private void Start()
    {
        LifeUIInstans();
    }

    /// <summary> ライフの更新 </summary> <param name="value"> -でダメージ　+で回復 </param>
    public void LifeUpdate(int value)
    {
        if (m_currentlife + value > m_maxLife) m_currentlife = m_maxLife;
        else if (m_currentlife + value < 0) m_currentlife = 0;
        else m_currentlife += value;

        if (m_currentlife <= 0)
        {
            LevelManager.Instance.GameOver();
        }
    }

    /// <summary> HPの表示をするオブジェクトをインスタンスします </summary>
    private void LifeUIInstans()
    {
        for (int i = 0; i < m_maxLife; i++)
        {
            GameObject lifeUI = Instantiate(m_lifeUIPrefab);
            lifeUI.transform.parent = m_lifeParentTransform;
            lifeUI.transform.localPosition = Vector3.zero;
            lifeUI.transform.localScale = m_lifeUiSize;
        }
    }
}