using System;
using System.Collections;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    [SerializeField] private float m_hitArea = 2;

    [SerializeField] Sprite m_floworSprite;

    [SerializeField] string m_brokenSe = "Broken";

    Animator m_anim;

    CircleCollider2D m_col;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_col = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.Instance.GameClear();
            m_col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            LevelManager.Instance.GameClear();
            m_col.isTrigger = true;
        }
    }
}