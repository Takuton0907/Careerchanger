using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTouch : MonoBehaviour
{
    Button m_button = null;

    private void Start()
    {
        m_button = GetComponent<Button>();
    }

    public void OnclickTouch()
    {
        m_button.interactable = false;
    }
}