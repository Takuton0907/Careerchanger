using UnityEngine;
using UnityEngine.UI;

/// <summary> ライフの変化時に演出を加える役割 </summary>
public class LifeCon : MonoBehaviour
{
    LifeManager m_lifeManager = null;
    LifeButtons lifeButtons;

    private void Start()
    {
        m_lifeManager = LevelManager.Instance.LifeManager;
        Debug.Log(m_lifeManager.GetLifeUIObjects);
        lifeButtons = new LifeButtons(m_lifeManager.GetLifeUIObjects.Length);
        GetButtons();
    }

    public void LifeUIUpdate(int value)
    {
        if (value == 0) return;

        if (value <= -1)
        {
            DamageEffect(value);
        }
        else
        {
            HealEffect(value);
        }
    }

    /// <summary> プレイヤーがダメージを受けた時の処理 </summary>
    private void DamageEffect(int value)
    {
        int life = m_lifeManager.GetCurrentLife;
        for (int i = 0; i < Mathf.Abs(value); i++)
        {
            lifeButtons[life - i].interactable = false;
        }
    }
    /// <summary> プレイヤーが回復をした時の処理 </summary>
    private void HealEffect(int value)
    {
        int life = m_lifeManager.GetCurrentLife;
        for (int i = 0; i < value; i++)
        {
            lifeButtons[life - 1 - i].interactable = true;
        }
    }
    /// <summary> LifeManagerが持つGameObjectからButtonコンポーネントを取得 </summary>
    private void GetButtons()
    {
        int i = 0;
        foreach (var item in m_lifeManager.GetLifeUIObjects)
        {
            item.TryGetComponent(out Button button);
            lifeButtons[i] = button;
            i++;
        }
    }
}

class LifeButtons
{
    Button[] buttons;

    public LifeButtons(int buttonArraySize)
    {
        buttons = new Button[buttonArraySize];
    }

    public Button this [int index]
    {
        get {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= buttons.Length)
            {
                index = buttons.Length - 1;
            }
            return buttons[index];
        }
        set {
            buttons[index] = value;
        }
    }
}