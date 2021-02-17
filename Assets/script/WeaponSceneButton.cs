﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSceneButton : MonoBehaviour
{
    [SerializeField] Image WeaponImage;
    [SerializeField] Button[] buttons = new Button[0];
    [SerializeField] GameObject[] Select = new GameObject[0];
    [SerializeField] float StartFadeInterval = 2;
    [SerializeField] float BackFadeInterval = 2;

    [SerializeField] string m_BgmName = "小さな冒険";
    [SerializeField] string m_tochSe = "sound_ok";

    DragObj dragObj;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(m_BgmName);
    }

    public void OnClickBack()
    {
        AudioManager.Instance.PlaySE(m_tochSe);
        FadeManager.Instance.LoadScene("StageSelect", BackFadeInterval);
    }

    public void OnclickWeapon(GameObject obj)
    {
        AudioManager.Instance.PlaySE(m_tochSe);
        string imagePath = string.Empty; 
        dragObj = obj.GetComponent<DragObj>();
        switch (dragObj.weapon)
        {
            case AttackMode.Sword:
                imagePath = "Sprite/sword";
                break;
            case AttackMode.Spear:
                imagePath = "Sprite/lance";
                break;
            case AttackMode.Axe:
                imagePath = "Sprite/ax";
                break;
            case AttackMode.Staff:
                imagePath = "Sprite/cane";
                break;
            case AttackMode.Bow:
                imagePath = "Sprite/bow";
                break;
            case AttackMode.Katana:
                imagePath = "Sprite/tachi";
                break;
        }
        Sprite nextImage = Resources.Load<Sprite>(imagePath);
        if (nextImage != null)
        {
            WeaponImage.sprite = nextImage;
        }
    }

    public void WeaponsInteract()
    {
        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }

    public void OnclickPlay()
    {
        bool playJudge = true;
        List<AttackMode> attackModes = new List<AttackMode>();
        foreach (var item in Select)
        {
            playJudge = item.HasChild();
            if (playJudge)
            {
                attackModes.Add(item.GetComponentInChildren<DragObj>().weapon);
            }
        }

        if (playJudge)
        {
            FlagManager.SetWeapon(DataManager.Instance.GetStage().stageNum, attackModes.ToArray());
            FadeManager.Instance.LoadScene("Game", StartFadeInterval);
        }
    }
}
