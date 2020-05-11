﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSceneButton : MonoBehaviour
{
    [SerializeField] Image WeaponImage;
    [SerializeField] Text WeaponText ;
    [SerializeField] TextAsset WeaponMasterDate;
    [SerializeField] Button[] buttons = new Button[0];
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
        SceneManager.LoadScene("StageSelect");
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
                buttons[i].image.sprite = Resources.Load<Sprite>("Sprite/" + (Weapons.Sword + i).ToString() + "Sprite");
            }
            else
            {
                buttons[i].image.sprite = Resources.Load<Sprite>("Sprite/" + (Weapons.Sword + i).ToString() + "_Push");
            }
        }
    }
}
