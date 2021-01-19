using UnityEngine;

//行動変更時の変更
public abstract class MovingObject : MonoBehaviour
{
    //止めた時のvelocityの一時的保存場所
    Vector2 m_localVelocity;

    protected Rigidbody2D m_rig;

    protected Animator m_animator;

    [SerializeField] 
    protected MoveState m_moveState = MoveState.Init;

    protected virtual void Awake()
    {
        m_rig = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    protected abstract void Move();

    public void ManagedUpdate()
    {
        switch (m_moveState)
        {
            case MoveState.Stop:
                break;
            case MoveState.Action:
                Move();
                break;
            default:
                break;
        }
    }

    public void MoveStart()
    {
        m_animator.SetFloat("Multiplier", 1.0f);
        
        m_rig.velocity = m_localVelocity;
        m_rig.gravityScale = 1;
        m_moveState = MoveState.Action;
    }

    public void MoveStop()
    {
        m_animator.SetFloat("Multiplier", 0f);

        m_localVelocity = m_rig.velocity;
        m_rig.velocity = Vector2.zero;
        m_moveState = MoveState.Stop;
        m_rig.gravityScale = 0;
    }

    public void AnimationOnlyStart()
    {
        m_animator.SetFloat("Multiplier", 1.0f);
    }
}

//enemyのstate管理
public enum MoveState
{
    Init,
    Stop,
    Action,
}