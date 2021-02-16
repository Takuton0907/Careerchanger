using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGroundManager : MonoBehaviour
{
    [Header("背景画像")]
    [SerializeField] GameObject m_cloud = null;

    [Header("雲の動くスピード")]
    [SerializeField] float m_cloudSpeed = -1.3f;

    Transform[] m_repeatedlyImages = new Transform[3];

    float m_cloudRetunePosiX = 20;
    /// <summary> プレイヤーのTransform </summary>
    Transform m_playerTrans = default;

    private void Awake()
    {
        LevelManager.Instance.BackGroundManager = this;
    }

    private void Start()
    {
        m_repeatedlyImages = DuplicatBackGrounds(m_cloud, 3);

        m_playerTrans = LevelManager.Instance.PlayerCon.transform;

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
    private GameObject DuplicationSprite(GameObject backGround, float space)
    {
        Vector3 instancePosi = new Vector3(backGround.transform.position.x + space, backGround.transform.position.y, backGround.transform.position.z);
        GameObject obj = Instantiate(backGround, instancePosi, Quaternion.identity, backGround.transform.parent);
        return obj;
    }

    private Transform[] DuplicatBackGrounds(GameObject obje, int num)
    {
        Transform[] transforms = new Transform[num];

        if (obje.TryGetComponent(out SpriteRenderer s))//スプライトの場合の計算
        {
            m_cloudRetunePosiX = obje.transform.position.x + Mathf.Floor(s.size.x) * obje.transform.localScale.x;
        }


        transforms[0] = obje.transform;
        for (int i = 1; i < transforms.Length; i++)
        {
            transforms[i] = DuplicationSprite(transforms[i - 1].gameObject, m_cloudRetunePosiX).transform;
        }

        return transforms;
    }
}
