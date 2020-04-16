using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] float m_arrowXSpeed = 60;
    [SerializeField] float m_arrowYSpeed = 25;
    [SerializeField] bool m_angleArrow;
    Rigidbody2D m_rb2d;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        if (m_angleArrow)
        {
            m_rb2d.velocity = new Vector2(m_arrowXSpeed, m_arrowYSpeed);
        }
        else
        {
            m_rb2d.velocity = new Vector2(m_arrowXSpeed, 0);
        }
        
        StartCoroutine(KillMe());
    }

    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
