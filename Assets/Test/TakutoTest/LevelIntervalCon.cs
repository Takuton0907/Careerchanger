using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class LevelIntervalCon : MonoBehaviour
{
    GameObject m_vcam;

    CinemachineVirtualCamera m_startVcam;

    [SerializeField]
    Text m_countText;

    [SerializeField, Tooltip("ゲームスタートまでの時間")]
    float m_startInterval = 3;
    float m_currentLastTime;

    GameObject m_startletterObject;
    [SerializeField] float time = 5.0f;

    bool finPlayerAnim = false;

    private void Awake()
    {
        LevelManager.Instance.LevelIntervalCon = this;
        m_startInterval += 0.9f;
        m_vcam = GameObject.Find("StartVcam");
        m_startVcam = m_vcam?.GetComponent<CinemachineVirtualCamera>();

        m_startletterObject = GameObject.Find("GameStartObject");
        //m_mainPosition = new Vector3(-25.7f, 0f, -10f);
    }

    private void Start()
    {
        m_countText.gameObject.SetActive(false);
        //m_vcam.gameObject.SetActive(false); 
        if (m_startletterObject != null)
        {
            m_startletterObject.gameObject.SetActive(false);
        }

        GameObject player;
        player = LevelManager.Instance.PlayerCon.gameObject;
        if (m_startVcam != null)
        {
            m_startVcam.Follow = player.transform;
        }

        m_startVcam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0f, -3.0f);

        StartCoroutine(PlayerAnimWait());
    }

    public bool CountDown()
    {
        if (!finPlayerAnim) return false;

        m_countText.gameObject.SetActive(true);

        m_startInterval -= Time.deltaTime;
        m_currentLastTime = Mathf.FloorToInt(m_startInterval);
        m_countText.text = m_currentLastTime.ToString();

        if (m_startInterval < 1)
        {
            m_countText.gameObject.SetActive(false);
            if (m_startletterObject != null)
            {
                m_startletterObject.gameObject.SetActive(true);
            }
            StartCoroutine(ResetText());
            StartCoroutine(FadeOutGameStart(1));

            return true;
        }
        return false;
    }

    IEnumerator PlayerAnimWait()
    {
        yield return null;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        m_startVcam.Priority = 0;

        yield return new WaitForSeconds(4);

        finPlayerAnim = true;
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(2);
        m_countText.text = string.Empty;
    }

    IEnumerator FadeOutGameStart(float interval)
    {
        float time = 0;
        SpriteRenderer spriteRenderer = new SpriteRenderer();
        if (m_startletterObject != null)
        {
            spriteRenderer = m_startletterObject.GetComponent<SpriteRenderer>();
        }

        yield return new WaitForSeconds(2);

        while (time <= interval)
        {
            if (m_startletterObject != null)
            {
                spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, time / interval));
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
