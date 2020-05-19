using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static partial class GameObjectExtensions
{
    public static bool HasChild(this GameObject gameObject)
    {
        return 0 < gameObject.transform.childCount;
    }
}


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
        Fade.Instance.LoadScene(BackFadeInterval, "StageSelect");
    }

    public void OnclickWeapon(GameObject obj)
    {
        WeaponText.text = string.Empty;
        string imagePath = string.Empty; 
        dragObj = obj.GetComponent<DragObj>();
        weaponSpriteChange(dragObj.weapon);
        switch (dragObj.weapon)
        {
            case Weapons.Sword:
                imagePath = "Sprite/sword";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.Sword)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case Weapons.Lance:
                imagePath = "Sprite/lance";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.Lance)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case Weapons.axe:
                imagePath = "Sprite/ax";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.axe)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case Weapons.cane:
                imagePath = "Sprite/cane";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.cane)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case Weapons.bow:
                imagePath = "Sprite/bow";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.bow)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            case Weapons.tachi:
                imagePath = "Sprite/tachi";
                for (int i = 0; i < textDates.GetLength(0); i++)
                {
                    if (int.Parse(textDates[i, 0]) == (int)Weapons.tachi)
                    {
                        WeaponText.text += textDates[i, 1] + "\n";
                    }
                }
                break;
            default:
                break;
        }
        try
        {
            WeaponImage.sprite = Resources.Load<Sprite>(imagePath);
    }
        catch (System.Exception)
        {

        }
    }

    private void weaponSpriteChange(Weapons weapon)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if ((int)weapon != i)
            {
                buttons[i].GetComponentInChildren<Button>().image.sprite = Resources.Load<Sprite>("Sprite/" + (Weapons.Sword + i).ToString() + "Sprite");
            }
            else
            {
                buttons[i].GetComponentInChildren<Button>().image.sprite = Resources.Load<Sprite>("Sprite/" + (Weapons.Sword + i).ToString() + "_Push");
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
            switch (StageSceneButton.stage)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage1].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage1");
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage2].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage2");
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage3].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage3");
                    break;
                case 4:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage1_EX].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage1_");
                    break;
                case 5:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage2_EX].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage2_");
                    break;
                case 6:
                    for (int i = 0; i < 3; i++)
                    {
                        FlagManager.Flag[Stages.Stage3_EX].LastWeapon[i] = Select[i].GetComponentInChildren<DragObj>().weapon;
                    }
                    Fade.Instance.LoadScene(StartFadeInterval, "Stage3_");
                    break;
                default:
                    Debug.Log("不適切な数値が代入されました");
                    break;
            }
        }
    }
}
