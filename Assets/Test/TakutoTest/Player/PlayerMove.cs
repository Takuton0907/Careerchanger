using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerActionBase
{   
    /// <summary>プレイヤーが早くなっていくスピード</summary>
    [SerializeField] float m_speed = 3.0f;
    public override IEnumerator PlayerAction(float piece)
    {
        while (true)
        {
            //移動
            transform.Translate(new Vector2(piece, 0) * Time.deltaTime * m_speed);
            yield return null;
        }
    }
}
