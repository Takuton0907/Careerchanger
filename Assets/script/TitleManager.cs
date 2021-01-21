using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] float fadeInterval = 2;
    
    public void OnClickGamaStart()
    {
        FadeManager.Instance.LoadScene("StageSelect", fadeInterval);
    }
}
