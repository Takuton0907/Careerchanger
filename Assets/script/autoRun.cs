using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class autoRun : MonoBehaviour
{
    [SerializeField] float runSPD = 5f;
    [SerializeField] AudioClip sfx1; //効果音
    [SerializeField] AudioClip sfx2;

    AudioSource source;
    Rigidbody2D rd2d;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   //簡易版オートラン
        float v = Input.GetAxisRaw("Vertical");
        Vector2 vector2 = new Vector2(runSPD,v).normalized;
        rd2d.velocity = vector2 * runSPD;
    }
}
