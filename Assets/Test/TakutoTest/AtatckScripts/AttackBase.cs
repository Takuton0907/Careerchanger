using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] float m_recastTime = 1f;

    protected BoxCollider2D m_collider = null;
    abstract protected IEnumerator AttackCoroutine();
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    protected virtual void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
    }

    public float GetRecastTime() => m_recastTime;
}