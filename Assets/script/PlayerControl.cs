using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] float speed = 4;//通常のスピード
    [SerializeField] float jumpSpeed = 2;//ジャンプ中のスピード
    [SerializeField] float flightIntervalTime = 1;
    float flightTime;
    [SerializeField] float maxJump = 2;
    public Vector3 cameraAdjust;

    Camera mainCamera;
    Vector3 underPosi;
    const float piece = 1;//プログラム上での1マスの大きさ
    bool toJump = true;
    float time = 1;

    enum JumpSwitch
    {
        jump,
        move,
        dismount,
        onGround,
    };
    JumpSwitch jumpSwitch = JumpSwitch.dismount;
    // Start is called before the first frame update
    void Start()
    {
        flightTime = flightIntervalTime;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        transform.Translate(new Vector2(piece, 0) * Time.deltaTime * speed);
        if (time <= 0)
        {
            Debug.Log(transform.position);
            time = 1;
        }
        time -= Time.deltaTime;

        //カメラ操作
        mainCamera.transform.position = new Vector3(transform.position.x + cameraAdjust.x, cameraAdjust.y, cameraAdjust.z);

        //ジャンプ動作
        switch (jumpSwitch)
        {
            case JumpSwitch.jump:
                Debug.Log(underPosi);
                transform.Translate(new Vector2(0, piece * 2) * Time.deltaTime * jumpSpeed);
                if (transform.position.y >= underPosi.y + maxJump)
                {
                    jumpSwitch = JumpSwitch.move;
                }
                break;
            case JumpSwitch.move:
                flightIntervalTime -= Time.deltaTime;
                if (flightIntervalTime <= 0)
                {
                    jumpSwitch = JumpSwitch.dismount;
                    flightIntervalTime = flightTime;
                }
                break;
            case JumpSwitch.dismount:
                transform.Translate(new Vector2(0, -piece * 2) * Time.deltaTime * jumpSpeed);
                if (!toJump)
                {
                    jumpSwitch = JumpSwitch.onGround;
                }
                break;
            case JumpSwitch.onGround:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    toJump = true;
                    underPosi = transform.position;
                    jumpSwitch = JumpSwitch.jump;
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            //Debug.Log("当たった");
            jumpSwitch = JumpSwitch.onGround;
            //underPosi = transform.position;
            toJump = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (hp > 0)
            {
                hp -= 1;
                Debug.Log("player is damage");
            }
            else
            {
                Debug.Log("player is destroy");
                Destroy(this.gameObject);
            }
        }
    }
}
