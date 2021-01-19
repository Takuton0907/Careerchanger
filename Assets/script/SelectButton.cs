using UnityEngine;
using UnityEngine.UI;

//inspecter上でステージを変更しそのステージを後でセットする
public class SelectButton : MonoBehaviour
{
    [SerializeField]
    Stage m_stage = Stage.Stage1;

    private void Start()
    {
        if (!DataManager.Instance.OpneStageCheck(m_stage))
        {
            GetComponent<Button>().interactable = false;
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