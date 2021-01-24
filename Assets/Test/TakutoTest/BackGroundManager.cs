using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGroundManager : MonoBehaviour
{
    [Header("背景画像")]
    [SerializeField] GameObject m_sky = null;
    [SerializeField] GameObject m_cloud = null;
    [SerializeField] GameObject m_middle = null; 
    [SerializeField] GameObject m_front = null;

    [Header("雲の動くスピード")]
    [SerializeField] float m_cloudSpeed = -1.3f;
    [SerializeField] float m_playerSpeedAdd = 0.2f;

    [Header("クリア時までの背景調整")]
    [SerializeField] float m_clearOffset = 30;

    Transform[] m_repeatedlyImages = new Transform[2];

    float m_cloudRetunePosiX = 20;
    /// <summary> プレイヤーのTransform </summary>
    Transform m_playerTrans = default;
    /// <summary> プレイヤーのRigidbody </summary>
    Rigidbody2D m_playerRig = default;
    /// <summary> ステージ全体の長さ </summary>
    float m_stageDistance = default;
    /// <summary> スタートのポジション </summary>
    Vector3 m_startPosi = default;
    /// <summary> ゴールのポジション </summary>
    Vector3 m_clearPosi = default;

    Vector3 m_middleStartPosi;
    Vector3 m_frontStartPosi;
    Vector3 m_skyStartPosi;

    private void Awake()
    {
        LevelManager.Instance.BackGroundManager = this;
    }

    private void Start()
    {
        m_repeatedlyImages[0] = m_cloud.transform;
        m_repeatedlyImages[1] = DuplicationSprite(m_cloud, ref m_cloudRetunePosiX).transform;

        m_middleStartPosi = m_middle.transform.position;
        m_frontStartPosi = m_front.transform.position;
        m_skyStartPosi = m_sky.transform.position;

        m_playerTrans = LevelManager.Instance.PlayerCon.transform;
        m_startPosi = m_playerTrans.position;
        m_clearPosi = FindObjectOfType<ClearObject>().transform.position;

        m_stageDistance = m_clearPosi.x - m_playerTrans.position.x;

        m_playerRig = LevelManager.Instance.PlayerCon.GetComponent<Rigidbody2D>();

        m_clearOffset = m_middle.GetComponent<SpriteRenderer>().size.x * m_middle.transform.localScale.x / 2 - m_clearOffset;
        
        BackgroundMove();
        RepeatedlyMove();
    }

    /// <summary> 画像を一定のスピードで動かし続けます </summary>
    public void RepeatedlyMove()
    {
        foreach (var cloud in m_repeatedlyImages)
        {
            cloud.Translate(m_cloudSpeed * Time.deltaTime, 0, 0);
            
            if (cloud.localPosition.x < -m_cloudRetunePosiX)
            {
                cloud.localPosition = new Vector3(m_cloudRetunePosiX - 0.1f, 0, cloud.position.z);
            }
        }
    }

    /// <summary> 三枚目以外を動かします </summary>
    public void BackgroundMove()
    {
        float playerProgress = (m_playerTrans.position.x - m_startPosi.x) / m_stageDistance;

        Vector3 start = m_startPosi + new Vector3(m_clearOffset, 0, 0);
        Vector3 gool = m_clearPosi - new Vector3(m_clearOffset / 5, 0, 0);

        Vector3 posi = Vector3.Lerp(start, gool, playerProgress);
        posi.z = m_middleStartPosi.z;
        posi.y = m_middleStartPosi.y;
        m_middle.transform.position = posi;

        posi = Vector3.Lerp(start, gool, playerProgress);
        posi.z = m_frontStartPosi.z;
        posi.y = m_frontStartPosi.y;
        m_front.transform.position = posi;

        posi = Vector3.Lerp(start, gool, playerProgress);
        posi.z = m_skyStartPosi.z;
        posi.y = m_skyStartPosi.y;
        m_sky.transform.position = posi;
    }

    /// <summary> 画像を複製して横に並べます </summary>
    private GameObject DuplicationSprite(GameObject backGround, ref float retunePosi)
    {
        if (backGround.TryGetComponent(out SpriteRenderer s))
        {
            retunePosi = backGround.transform.position.x + s.size.x * backGround.transform.localScale.x;

            Vector3 instancePosi = new Vector3(retunePosi, backGround.transform.position.y, backGround.transform.position.z);
            GameObject obj = Instantiate(backGround, instancePosi, Quaternion.identity, backGround.transform.parent);
            return obj;
        }
        return null;
    }
}
