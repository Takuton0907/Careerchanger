using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] GameObject background1;
    [SerializeField] GameObject background2;
    [SerializeField] GameObject player;
    [SerializeField] double IntervalTime = 10f;
    [SerializeField] double TimeBackgroundMove = 10f;
    [SerializeField] int BackgroundSize = 7;
    PlayerControl playerControl;
    int num =1;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        background1.transform.localPosition = new Vector3((num - 1) * BackgroundSize, background1.transform.localPosition.y, 10);
        background2.transform.localPosition = new Vector3(num * BackgroundSize, background2.transform.localPosition.y, 10);
    }

    // Update is called once per frame
    void Update()
    {
        TimeBackgroundMove -= Time.deltaTime;
        //if (TimeBackgroundMove < 0)
        if (player.transform.position.x  + playerControl.cameraAdjust.x >= num * BackgroundSize)
        {
            num++;
            background1.transform.localPosition = new Vector3((num - 1) * BackgroundSize, background1.transform.localPosition.y, 10);
            background2.transform.localPosition = new Vector3(num * BackgroundSize, background2.transform.localPosition.y, 10);
            TimeBackgroundMove = IntervalTime;
        }
    }
}
