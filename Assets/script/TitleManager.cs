using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] float fadeInterval = 2;

    [SerializeField] Animator m_animator = null;

    [SerializeField] string m_bgmNamae = "導きの旅人";

    [SerializeField] string m_tochSe = "sound_ok";

    private void Start()
    {
        AudioManager.Instance.PlayBGM(m_bgmNamae);
    }

    public void OnClickGamaStart()
    {
        FadeManager.Instance.LoadScene("StageSelect", fadeInterval);
        AudioManager.Instance.PlaySE(m_tochSe);
    }

    public void OnClickTouch()
    {
        m_animator?.SetTrigger("Tocuh");
        AudioManager.Instance.PlaySE(m_tochSe);
    }
}
