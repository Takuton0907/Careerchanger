using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    /// <summary> 表示するHPのUI Prefab </summary>
    [SerializeField] GameObject m_lifeUIPrefab = null;
    /// <summary> 最大HP </summary>
    [SerializeField] int m_maxLife = 3;
    /// <summary> UIのインスタンス時のサイズ </summary>
    [SerializeField] Vector3 m_lifeUiSize = Vector3.one;

    /// <summary> 最大のLifeを返します </summary>
    public int GetMaxLife { get { return m_maxLife; } }
    /// <summary> 現在のHPを返します </summary>
    public int GetCurrentLife { get { return m_currentlife; } }
    int m_currentlife; //現在のHP管理用

    public GameObject[] GetLifeUIObjects { get { return m_hpUiObjects; } }
    private GameObject[] m_hpUiObjects;

    LifeCon m_lifeCon = null;
    Transform m_lifeParentTransform = null;

    private void Awake()
    {
        LevelManager.Instance.LifeManager = this;

        m_lifeCon = FindObjectOfType<LifeCon>();
        if (m_lifeCon != null)
        {
            m_lifeParentTransform = m_lifeCon.transform;
        }

        LifeUIInit(m_maxLife);
    }

    /// <summary> ライフの更新 </summary> <param name="value"> -でダメージ　+で回復 </param>
    public void LifeUpdate(int value)
    {
        if (m_currentlife + value > m_maxLife) m_currentlife = m_maxLife;
        else if (m_currentlife + value < 0) m_currentlife = 0;
        else m_currentlife += value;

        m_lifeCon.LifeUIUpdate(value);

        LevelManager.Instance.ScoreManager?.ComboReset();

        if (m_currentlife <= 0)
        {
            LevelManager.Instance.GameOver();
        }
    }

    /// <summary> HPの表示をするオブジェクトをインスタンスする </summary>
    private void LifeUIInit(int maxlife)
    {
        m_currentlife = m_maxLife;

        if (m_lifeCon == null) return;

        m_hpUiObjects = new GameObject[maxlife];
        for (int i = 0; i < maxlife; i++)
        {
            GameObject lifeUI = Instantiate(m_lifeUIPrefab);
            lifeUI.transform.parent = m_lifeParentTransform;
            lifeUI.transform.localPosition = Vector3.zero;
            lifeUI.transform.localScale = m_lifeUiSize;
            m_hpUiObjects[i] = lifeUI;
        }
    }
}