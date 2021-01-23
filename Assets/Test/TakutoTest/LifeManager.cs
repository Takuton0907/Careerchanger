using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] int m_maxLife = 6;
    public int GetMaxLife { get { return m_maxLife; } }
    int m_currentlife;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.LifeManager = this;

        m_currentlife = m_maxLife;
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
}
