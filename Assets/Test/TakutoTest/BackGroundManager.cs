using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGroundManager : MonoBehaviour
{
    [Header("背景画像")]
    [SerializeField] GameObject m_cloud = null;

    [Header("雲の動くスピード")]
    [SerializeField] float m_cloudSpeed = -1.3f;
    [SerializeField] float m_playerSpeedAdd = 0.2f;

    Transform[] m_repeatedlyImages = new Transform[2];

    float m_cloudRetunePosiX = 20;
    /// <summary> プレイヤーのTransform </summary>
    Transform m_playerTrans = default;

    /// <summary> ゴールのポジション </summary>
    Vector3 m_clearPosi = default;

    private void Awake()
    {
        LevelManager.Instance.BackGroundManager = this;
    }

    private void Start()
    {
        m_repeatedlyImages[0] = m_cloud.transform;
        m_repeatedlyImages[1] = DuplicationSprite(m_cloud, ref m_cloudRetunePosiX).transform;

        m_playerTrans = LevelManager.Instance.PlayerCon.transform;
        m_clearPosi = FindObjectOfType<ClearObject>().transform.position;

        RepeatedlyMove();
    }

    /// <summary> 画像を一定のスピードで動かし続けます </summary>
    public void RepeatedlyMove()
    {
        for (int i = 0; i < m_repeatedlyImages.Length; i++)
        {
            m_repeatedlyImages[i].Translate(m_cloudSpeed * Time.deltaTime, 0, 0); 
            
            if (m_playerTrans.position.x - m_repeatedlyImages[i].position.x > m_cloudRetunePosiX)
            {
                int index = i - 1 >= 0 ? i - 1 : m_repeatedlyImages.Length - 1;
                float nextPositionX = m_repeatedlyImages[index].position.x + m_cloudRetunePosiX;
                m_repeatedlyImages[i].transform.position = new Vector3(nextPositionX - 0.1f, 0, m_repeatedlyImages[i].position.z);
            }
        }
    }

    /// <summary> 画像を複製して横に並べます </summary>
    private GameObject DuplicationSprite(GameObject backGround, ref float retunePosi)
    {
        if (backGround.TryGetComponent(out SpriteRenderer s))
        {
            retunePosi = backGround.transform.position.x + Mathf.Floor(s.size.x) * backGround.transform.localScale.x;
            
            Vector3 instancePosi = new Vector3(backGround.transform.position.x + retunePosi, backGround.transform.position.y, backGround.transform.position.z);
            GameObject obj = Instantiate(backGround, instancePosi, Quaternion.identity, backGround.transform.parent);
            return obj;
        }
        return null;
    }
}
