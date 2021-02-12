using UnityEngine;
using UnityEngine.UI;

//inspecter上でステージを変更しそのステージを後でセットする
public class SelectButton : MonoBehaviour
{
    [SerializeField]
    Stage m_stage = Stage.Stage1;

    [SerializeField]
    Sprite m_image = null;

    private void Start()
    {
        if (!DataManager.Instance.OpneStageCheck(m_stage))
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            if (!FlagManager.Flag[m_stage].Clear)
            {
                GetComponent<Image>().sprite = m_image;
            }
        }
    }

    public void OcClickGameStart()
    {
        if (SelectSceneManager.Instance != null)
        {
            SelectSceneManager.Instance.LevelSelect(m_stage);
        }
    }
}