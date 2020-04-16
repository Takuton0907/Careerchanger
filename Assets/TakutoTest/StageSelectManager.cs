using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] GameObject StageStartOpsion;

    public GameObject[] dummyStage = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        stageOpen(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stageOpen(int[] dummyNumber)
    {
        foreach (var item in dummyNumber)
        {
            dummyStage[item - 1].SetActive(false);
        }
    }

    public void stageOpen(int dummyNumber)
    {
       dummyStage[dummyNumber - 1].SetActive(false);  
    }

    public void OpenSelectWindow()
    {
        StageStartOpsion.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        StageStartOpsion.SetActive(false);
    }
}
