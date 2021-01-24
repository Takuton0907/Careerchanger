using System.Collections;
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
        /// <summary>死んだ状態（ゲームオーバー時のみ）</summary>
        Dead
    }

    [Header("SEの名前"), SerializeField]
    string m_damageSe = "damage";
    
    [Header("１マスの大きさ"), SerializeField] 
    private float m_piece = 1;//プログラム上での1マスの大きさ
    
    /// <summary>現在のステート</summary>
    PlayerState m_state;

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.PlayerCon = this;
    }
    void Start()
    {

    }
    public void Init()
    {
        ChangeState(PlayerState.Idle1);
    }
    public void Play()
    {
        ChangeState(PlayerState.Walk);
    }

    protected override void Move()
    {

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
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Damaged:
                m_rig.velocity = Vector2.zero;
                break;
            case PlayerState.Dead:
                m_rig.velocity = Vector2.zero;
                break;
            default:
                break;
        }
    }

    public void Clear()
    {

    }
    public void GameOver()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPlayerHit item))
        {
            item.Action();
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