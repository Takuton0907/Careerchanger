﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : PlayerBase
{
    /// <summary>プレイヤーのステート</summary>
    public enum PlayerState
    {
        /// <summary>ゲームスタート前の待機状態</summary>
        Idle1,
        /// <summary>クリスタル破壊時の待機状態</summary>
        Idle2,
        /// <summary>歩き状態</summary>
        Walk,
        /// <summary>走り状態</summary>
        Run,
        /// <summary>被ダメージ状態</summary>
        Damaged,
        /// <summary>ゲームクリア時の"喜び"状態</summary>
        Joy,
        /// <summary>死んだ状態（ゲームオーバー時のみ）</summary>
        Dead
    }
    /// <summary>「歩き」状態から「走り」状態になるまでの時間</summary>
    [SerializeField] float m_timeToChangeFromWalkToRun = 10.0f;
    /// <summary>プレイヤーが早くなっていくスピード</summary>
    [SerializeField] float m_speed = 3.0f;
    /// <summary>プレイヤーの歩く最大スピード</summary>
    [SerializeField] float m_maxWalkSpeed = 5.0f;
    /// <summary>プレイヤーの走る最大スピードの倍率</summary>
    [SerializeField] float m_multiplRunSpeed = 1.5f;
    float m_maxRunSpeed;
    /// <summary> ダメージ後の無敵時間 </summary>
    [SerializeField] float m_invincibleTime = 1;

    [Header("SEの名前")]
    [SerializeField] string m_damageSe = "damage";

    /// <summary>歩き状態が何秒継続したか。経過時間</summary>
    float m_elapsedTime = 0.0f;
    /// <summary>現在のステート</summary>
    PlayerState m_state;

    public bool GetInfiniteLine { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.PlayerCon = this;
        m_animator.SetFloat("Multiplier", 0f);
    }
    void Start()
    {
        m_maxRunSpeed = m_maxWalkSpeed * m_multiplRunSpeed;
    }
    public void Init()
    {
        ChangeState(PlayerState.Idle1);
        m_animator.SetFloat("Multiplier", 1f);
    }
    public void Play()
    {
        ChangeState(PlayerState.Walk);
    }

    float flightTime;
    [SerializeField] float maxJump = 2;
    public Vector3 cameraAdjust;

    Vector3 underPosi;
    const float piece = 1;//プログラム上での1マスの大きさ
    bool toJump = true;
    float time = 1;
    enum JumpState
    {
        jump,
        move,
        dismount,
        onGround,
    };

    JumpState jumpSwitch = JumpState.dismount;
    protected override void Move()
    {
        //移動
        transform.Translate(new Vector2(piece, 0) * Time.deltaTime * m_speed);
        if (time <= 0)
        {
            Debug.Log(transform.position);
            time = 1;
        }
        time -= Time.deltaTime;
    }

    //プレイヤーのState切り替え
    private void ChangeState(PlayerState nextState)
    {
        switch (m_state)
        {
            case PlayerState.Idle1:
                break;
            case PlayerState.Idle2:
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Damaged:
                break;
            case PlayerState.Joy:
                break;
            case PlayerState.Dead:
                break;
            default:
                break;
        }

        m_state = nextState;
        Debug.Log($"プレイヤーの現在のState {m_state}");

        switch (m_state)
        {
            case PlayerState.Idle1:
                m_rig.velocity = Vector2.zero;
                break;
            case PlayerState.Idle2:
                m_animator.SetTrigger("GameClear");
                break;
            case PlayerState.Walk:
                m_animator.SetTrigger("Walk");
                break;
            case PlayerState.Run:
                m_animator.SetTrigger("Run");
                break;
            case PlayerState.Damaged:
                m_rig.velocity = Vector2.zero;
                m_animator.SetTrigger("Damage");
                break;
            case PlayerState.Joy:
                m_rig.velocity = Vector2.zero;
                m_animator.SetTrigger("Joy");
                break;
            case PlayerState.Dead:
                m_rig.velocity = Vector2.zero;
                break;
            default:
                break;
        }

        m_elapsedTime = 0;
    }

    public void Clear()
    {
        ChangeState(PlayerState.Idle2);
    }
    public void GameOver()
    {
        ChangeState(PlayerState.Dead);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPlayerHit item))
        {
            item.Action();
        }

        if (collision.tag == "Ground")
        {
            //Debug.Log("当たった");
            jumpSwitch = JumpState.onGround;
            //underPosi = transform.position;
            toJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPlayerHit item))
        {
            item.Action();
        }
    }
}