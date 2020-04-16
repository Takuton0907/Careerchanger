using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyCon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioClip atackSFX;
    [SerializeField] AudioClip dieSFX;
    AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        if (player == null)
        {
            Debug.Log("player項目がセットされてません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //playerのオブジェクト名が決定したら書く
        player = GameObject.Find("player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Destroy(this.gameObject);
            Debug.Log("enemy destroy");
        }
    }
}
