using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] float fadeInterval = 2;
    
    public void OnClickGamaStart()
    {
        Fade.Instance.LoadScene(fadeInterval, "StageSelect");
    }
}
