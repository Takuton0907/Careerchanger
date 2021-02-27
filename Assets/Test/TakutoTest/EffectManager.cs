using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //敵に当たった時のエフェクト
    [SerializeField, Header("敵に当たった時のエフェクト")]
    GameObject m_hitImage;
    ObjectPool m_hitImagePool;
    Renderer[] renderers;

    //敵が死んだ時のエフェクト
    [SerializeField, Header("敵が死んだ時のエフェクト")]
    GameObject m_deathEffect;
    ObjectPool m_dearhEffectPool;

    [SerializeField, Header("クリア時などの紙吹雪")]
    GameObject m_confettiEffect;
    GameObject m_confettiEffectObj;

    private void Awake()
    {
        LevelManager.Instance.EffectManager = this;

        HitEffectSetup();
        DeathEffectSetup();
        ConfettiEffectSetup();
    }

    public void ManagedUpdate()
    {

    }

    /// <summary> 敵に当たった時のエフェクトの初期化 </summary>
    private void HitEffectSetup()
    {
        m_hitImagePool = new ObjectPool();
        m_hitImagePool.Pool(gameObject, m_hitImage, 8);
        renderers = new Renderer[m_hitImagePool.pool.Count];
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = m_hitImagePool.pool[i].GetComponent<Renderer>();
        }
    }
    /// <summary> 敵に攻撃したときのエフェクト </summary>
    public void InstanceHitEffect(Color color, Vector3 posi, Vector3 size)
    {
        if (m_hitImagePool.currentCount >= m_hitImagePool.pool.Count)
            renderers[0].material.color = color;
        else
            renderers[m_hitImagePool.currentCount].material.color = color;

        m_hitImagePool.Instantiate(posi + new Vector3(0, 0, 1), size);
    }

    /// <summary> 敵が死んだ時のエフェクトの初期化 </summary>
    private void DeathEffectSetup()
    {
        m_dearhEffectPool = new ObjectPool();
        m_dearhEffectPool.Pool(gameObject, m_deathEffect, 8);
    }
    /// <summary> 敵が死んだ時のエフェクト </summary>
    public void InstanceDeathEffect(Vector3 posi, Vector3 size) => m_dearhEffectPool.Instantiate(posi, size);

    private void ConfettiEffectSetup()
    {
        m_confettiEffectObj = Object.Instantiate(m_confettiEffect, transform.position, Quaternion.identity);
        m_confettiEffectObj.transform.SetParent(transform, false);
        m_confettiEffectObj.SetActive(false);
    }
    public void InstanceConfettiEffect()
    {
        Vector3 posi = Camera.main.transform.position;

        posi.z = transform.position.z;

        m_confettiEffectObj.SetActive(true);
        m_confettiEffectObj.transform.position = posi;
    }
}
