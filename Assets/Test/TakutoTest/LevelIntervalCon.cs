using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class LevelIntervalCon : MonoBehaviour
{
    [SerializeField] Text m_countText;

    [SerializeField, Tooltip("ゲームスタートまでの時間")]
    float m_startInterval = 3;
    float m_currentLastTime;

    GameObject m_startletterObject;
    [SerializeField] float m_startFadeTime = 5.0f;

    private void Awake()
    {
        LevelManager.Instance.LevelIntervalCon = this;
        m_startInterval += 0.9f;

        m_startletterObject = GameObject.Find("GameStartObject");
    }

    private void Start()
    {
        m_countText.gameObject.SetActive(false);
        if (m_startletterObject != null)
        {
            m_startletterObject.gameObject.SetActive(false);
        }
    }

    public bool CountDown()
    {
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
            StartCoroutine(FadeOutGameStart(m_startFadeTime));

            return true;
        }
        return false;
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
