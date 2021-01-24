using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerActionBase
{
    float flightTime;
    [SerializeField] float maxJump = 2;
    [SerializeField] float jumpSpeed = 2;//ジャンプ中のスピード
    [SerializeField] float flightIntervalTime = 1;
    public Vector3 cameraAdjust;

    Vector3 underPosi;
    const float piece = 1;//プログラム上での1マスの大きさ
    bool toJump = true;

    enum JumpState
    {
        jump,
        move,
        dismount,
        onGround,
    };

    JumpState jumpSwitch = JumpState.dismount;
    
    private void Start()
    {
        flightTime = flightIntervalTime;
    }

    public override IEnumerator PlayerAction()
    {
        yield return null;
        while (true)
        {
            //ジャンプ動作
            switch (jumpSwitch)
            {
                case JumpState.jump:
                    Debug.Log(underPosi);
                    transform.Translate(new Vector2(0, piece * 2) * Time.deltaTime * jumpSpeed);
                    if (transform.position.y >= underPosi.y + maxJump)
                    {
                        jumpSwitch = JumpState.move;
                    }
                    break;
                case JumpState.move:
                    flightIntervalTime -= Time.deltaTime;
                    if (flightIntervalTime <= 0)
                    {
                        jumpSwitch = JumpState.dismount;
                        flightIntervalTime = flightTime;
                    }
                    break;
                case JumpState.dismount:
                    transform.Translate(new Vector2(0, -piece * 2) * Time.deltaTime * jumpSpeed);
                    if (!toJump)
                    {
                        jumpSwitch = JumpState.onGround;
                    }
                    break;
                case JumpState.onGround:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        toJump = true;
                        underPosi = transform.position;
                        jumpSwitch = JumpState.jump;
                    }
                    break;
            }
        }
    }
}
