using System.Collections;
using UnityEngine;
using Cinemachine;

public class ClearPerformancesCon : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_mainVcam = null;
    [SerializeField] CinemachineBrain m_brain = null;

    CinemachineVirtualCamera m_clearVcam = null;
    ClearObject m_clearObject;


    private void Start()
    {
        m_clearVcam = GetComponent<CinemachineVirtualCamera>();
        m_clearObject = FindObjectOfType<ClearObject>();

        if (m_clearObject != null)
        {
            m_clearVcam.Follow = m_clearObject.transform;
            m_clearVcam.transform.position = new Vector3(m_clearObject.transform.position.x, m_clearObject.transform.position.y, transform.position.z);
            m_clearVcam.gameObject.SetActive(false);
        }
    }

    public IEnumerator GetFlower(float fadeTime)
    {
        if (m_clearVcam == null) yield break;

        m_clearVcam.gameObject.SetActive(true);

        m_mainVcam.Follow = null;

        m_brain.m_DefaultBlend.m_Time = fadeTime;

        m_clearVcam.Priority = m_mainVcam.Priority + 1;

        //yield return LevelManager.Instance.PlayerCon.ClearPlayerMove(m_brain.m_DefaultBlend.m_Time);

        Debug.Log("花獲得");
        yield return null;
    }
}
