using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSceneButton : MonoBehaviour
{
    [SerializeField] Image WeaponImage;
    [SerializeField] Sprite[] images;
    [SerializeField] Button[] buttons = new Button[0];
    [SerializeField] GameObject[] Select = new GameObject[0];
    [SerializeField] float StartFadeInterval = 2;
    [SerializeField] float BackFadeInterval = 2;

    [SerializeField] Image[] combos;
    [SerializeField] Sprite[] comboimages;

    [SerializeField] string m_BgmName = "小さな冒険";
    [SerializeField] string m_tochSe = "sound_ok";

    DragObj dragObj;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(m_BgmName);
        foreach (var item in combos)
        {
            item.color = Color.clear;
        }
    }

    public void OnClickBack()
    {
        AudioManager.Instance.PlaySE(m_tochSe);
        FadeManager.Instance.LoadScene("StageSelect", BackFadeInterval);
    }

    public void OnclickWeapon(GameObject obj)
    {
        AudioManager.Instance.PlaySE(m_tochSe);
        dragObj = obj.GetComponent<DragObj>();
        switch (dragObj.weapon)
        {
            case AttackMode.Sword:
                WeaponImage.sprite = images[0];
                break;
            case AttackMode.Spear:
                WeaponImage.sprite = images[1];
                break;
            case AttackMode.Axe:
                WeaponImage.sprite = images[2];
                break;
            case AttackMode.Staff:
                WeaponImage.sprite = images[3];
                break;
            case AttackMode.Bow:
                WeaponImage.sprite = images[4];
                break;
            case AttackMode.Katana:
                WeaponImage.sprite = images[5];
                break;
        }
        ComboData combo = DataManager.Instance.GetComboData(dragObj.weapon);
        for (int i = 0; i < combos.Length; i++)
        {
            if (combo.GetCombos().Count == 0)
            {
                combos[i].color = Color.clear;
            }
            else
            {
                combos[i].sprite = GetComboImage(combo.GetCombos()[0].combos[i]);
                combos[i].color = Color.white;
            }
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

    private Sprite GetComboImage(AttackMode attackMode)
    {
        Sprite sprite = default;
        switch (attackMode)
        {
            case AttackMode.Sword:
                sprite = comboimages[0];
                break;
            case AttackMode.Axe:
                sprite = comboimages[1];
                break;
            case AttackMode.Spear:
                sprite = comboimages[2];
                break;
            case AttackMode.Staff:
                sprite = comboimages[3];
                break;
            case AttackMode.Katana:
                sprite = comboimages[4];
                break;
            case AttackMode.Bow:
                sprite = comboimages[5];
                break;
        }
        return sprite;
    }
}
