using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerJump))]
public class PlayerCon : MovingObject
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

    [Header("SEの名前"), SerializeField] string m_damageSe = "damage";
    [Header("１マスの大きさ"), SerializeField] private float m_piece = 1;
    [Header("プレイヤーが早くなっていくスピード"), SerializeField] float m_speed = 3.0f;
    
    /// <summary>現在のステート</summary>
    PlayerState m_state;

    PlayerJump m_playerJump = null;

    protected override void Awake()
    {
        base.Awake();
        m_playerJump = GetComponent<PlayerJump>();
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
        //移動
        transform.Translate(new Vector2(m_piece, 0) * Time.deltaTime * m_speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
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
    }

    public void Clear()
    {

    }
    public void GameOver()
    {

    }

    public void Jump()
    {
        m_playerJump.RunAction(m_piece);
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