using UnityEngine;

//行動変更時の変更
public abstract class MovingObject : MonoBehaviour
{
    protected Animator m_animator;

    [SerializeField] 
    protected MoveState m_moveState = MoveState.Init;

    protected virtual void Awake()
    {
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
        m_animator?.SetFloat("Multiplier", 1.0f);
        m_moveState = MoveState.Action;
    }

    public void MoveStop()
    {
        m_animator?.SetFloat("Multiplier", 0f);

        m_moveState = MoveState.Stop;
    }

    public void AnimationOnlyStart()
    {
        m_animator?.SetFloat("Multiplier", 1.0f);
    }
}

//enemyのstate管理
public enum MoveState
{
    Init,
    Stop,
    Action,
}