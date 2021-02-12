using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] float fadeInterval = 2;

    [SerializeField] Animator m_animator = null;

    public void OnClickGamaStart()
    {
        FadeManager.Instance.LoadScene("StageSelect", fadeInterval);
    }

    public void OnClickTouch()
    {
        m_animator?.SetTrigger("Tocuh");
    }
}
