using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSceneButton : MonoBehaviour
{
    [SerializeField] Image WeaponImage;
    [SerializeField] Text WeaponText ;
    [SerializeField] TextAsset WeaponMasterDate;
    [SerializeField] GameObject[] buttons = new GameObject[0];
    [SerializeField] GameObject[] Select = new GameObject[0];
    [SerializeField] float StartFadeInterval = 2;
    [SerializeField] float BackFadeInterval = 2;

    LoadText loadText = new LoadText();
    DragObj dragObj;

    string[,] textDates;

    private void Start()
    {
        string date = loadText.LoadTextDate("Text/" + WeaponMasterDate.name);
        textDates = loadText.SetTexts(date);
    }

    public void OnClickBack()
    {
        FadeManager.Instance.LoadScene("StageSelect", BackFadeInterval);
    }

    public void OnclickWeapon(GameObject obj)
    {
        WeaponText.text = string.Empty;
        string imagePath = string.Empty; 
        dragObj = obj.GetComponent<DragObj>();
        WeaponSpriteChange(dragObj.weapon);
        switch (dragObj.weapon)
        {
            case AttackMode.Sword:
                imagePath = "Sprite/sword";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Sword)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case AttackMode.Spear:
                imagePath = "Sprite/lance";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Spear)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case AttackMode.Axe:
                imagePath = "Sprite/ax";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Axe)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case AttackMode.Staff:
                imagePath = "Sprite/cane";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Staff)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case AttackMode.Bow:
                imagePath = "Sprite/bow";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Bow)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case AttackMode.Katana:
                imagePath = "Sprite/tachi";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)AttackMode.Katana)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            default:
                break;
        }
        Sprite nextImage = Resources.Load<Sprite>(imagePath);
        if (nextImage != null)
        {
            WeaponImage.sprite = nextImage;
        }
    }

    private void WeaponSpriteChange(AttackMode weapon)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if ((int)weapon != i)
            {
                buttons[i].GetComponentInChildren<Button>().image.sprite = Resources.Load<Sprite>("Sprite/" + (AttackMode.Sword + i).ToString() + "Sprite");
            }
            else
            {
                buttons[i].GetComponentInChildren<Button>().image.sprite = Resources.Load<Sprite>("Sprite/" + (AttackMode.Sword + i).ToString() + "_Push");
            }
        }
    }

    public void OnclickPlay()
    {
        bool playJudge = true;
        foreach (var item in Select)
        {
            playJudge = item.HasChild();
        }

        if (playJudge)
        {
            FadeManager.Instance.LoadScene("Game", 2);
        }
    }
}
