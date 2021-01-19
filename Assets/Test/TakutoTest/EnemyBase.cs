using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MovingObject
{
    [SerializeField] bool m_debug = true;

    /// <summary> 敵の全撃破に関係するかしないかの判定用 </summary>
    public bool KillCount { private set; get; } = true;

    protected EnemyAction m_actionState = EnemyAction.Run;
    
    [Header("Status")]
    /// <summary> HPの最大値 </summary>
    [SerializeField] protected int m_maxLife = 10;
    /// <summary> 現在のlife </summary>
    private int m_currentLife;

    [SerializeField] protected float m_maxSpeed = 2;

    [Header("Audio")]
    /// <summary> 死亡時に流すサウンド </summary>
    [SerializeField] private string m_desClipName = "ClipName";
    /// <summary> ダメージを受けた時に流すサウンド </summary>
    [SerializeField] private string m_dmageClipName = "ClipName";
    [Header("ダメージ関連")]
    /// <summary> ダメージを受ける範囲 </summary>
    [SerializeField] private float m_hitArea = 2;
    /// <summary> ダメージを受けてから歩くまでの時間 </summary>
    [SerializeField] private float m_damageTime = 1.5f;
    /// <summary> 死ぬまでの時間 </summary>
    [SerializeField] private float m_deathTime = 1.5f;

    private bool m_hit = false;

    EnemyManager m_enemyManager;

    Coroutine m_animetionCoroutine;

    /// <summary> 敵の動作(Update) </summary>
    protected override abstract void Move();

    protected virtual void OnEnable()
    {
        StateChange(MoveState.Action);
        m_animator.SetInteger("Life", m_currentLife);
    }
    protected virtual new void Awake()
    {
        base.Awake();
        m_animator.SetInteger("Life", m_currentLife);
    }
    protected virtual void Start()
    {
        StateChange(MoveState.Init);
        ActionStateChange(EnemyAction.Run);
        m_enemyManager = LevelManager.Instance.EnemyManager;
        if (m_enemyManager != null) m_enemyManager.AddEnemy(this);
        else Debug.LogWarning($"EnemyManagerがありません");

        if (m_debug)
        {
            StateChange(MoveState.Action);
            Setup();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void OnDisable()
    {
        StateChange(MoveState.Stop);
    }

    //ダメージ
    public virtual void Damage(int value)
    {
        if (m_actionState == EnemyAction.Death) return;
        if (m_animetionCoroutine != null) StopCoroutine(m_animetionCoroutine);

        m_currentLife -= value;
        m_animator.SetInteger("Life", m_currentLife);
        if (m_actionState != EnemyAction.Attack) ActionStateChange(EnemyAction.Damage);
        if (m_currentLife <= 0) m_animetionCoroutine = StartCoroutine(DeathEnemy());
        else m_animetionCoroutine = StartCoroutine(Damaged());
        AudioManager.Instance.PlaySE(m_dmageClipName);
        Debug.Log($"{gameObject.name} m_currentLife = {m_currentLife}");
    }
    private IEnumerator Damaged()
    {
        yield return new WaitForSeconds(m_damageTime);
        ActionStateChange(EnemyAction.Run);
    }
    private IEnumerator DeathEnemy()
    {
        LevelManager.Instance.ScoreManager.KillEnemyCount(1);

        ActionStateChange(EnemyAction.Death);

        AudioManager.Instance.PlaySE(m_desClipName);

        yield return new WaitForSeconds(m_deathTime);

        LevelManager.Instance.EffectManager?.InstanceDeathEffect(transform.position, Vector3.one);

        m_enemyManager.RemoveEnemy(this);
        gameObject.SetActive(false);
    }
    //行動開始時のアクション
    private void Setup()
    {
        m_currentLife = m_maxLife;
    }
    //State変更
    public void StateChange(MoveState nextState)
    {
        switch (m_moveState)
        {
            case MoveState.Init:
                Setup();
                break;
            case MoveState.Stop:
                break;
            case MoveState.Action:
                break;
            default:
                break;
        }

        m_moveState = nextState;

        switch (m_moveState)
        {
            case MoveState.Init:
                break;
            case MoveState.Stop:
                MoveStop();
                break;
            case MoveState.Action:
                MoveStart();
                break;
            default:
                break;
        }
    }
    public void ActionStateChange(EnemyAction nextState)
    {
        m_actionState = nextState;

        switch (m_actionState)
        {
            case EnemyAction.Run:
                break;
            case EnemyAction.Attack:
                m_animator.SetTrigger("Attack");
                break;
            case EnemyAction.Damage:
                m_animator.SetTrigger("Damage");
                break;
            case EnemyAction.Death:
                break;
        }
    }

    //interfaceによる実装
    public virtual void LineHitAction(Vector3 touchPosi)
    {
        if (m_hit) return;
        if (m_currentLife <= 0) return;
        if (m_actionState == EnemyAction.Death) return;
        if (((Vector2)touchPosi - (Vector2)transform.position).magnitude > m_hitArea) return;

        m_hit = true;
    }  
    public void LineHitSetup()
    {
        m_hit = false;
    }
    public void LineHitEnd()
    {
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, m_hitArea);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_hitArea);
    }
#endif
}

//Enemyの行動
public enum EnemyAction
{
    Run,
    Attack,
    Damage,
    Death,
}