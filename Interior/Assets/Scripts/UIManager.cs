using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public Text debugText;

    public Text trackingText;
    public Button putButton;
    public Text[] unitTexts;

    public Color mainUIColor;
    
    private Dictionary<ApplicationInfo.Unit, Text> buttons;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (unitTexts != null)
        {
            buttons = new Dictionary<ApplicationInfo.Unit, Text>(3);
            buttons.Add(ApplicationInfo.Unit.mm, unitTexts[0]);
            buttons.Add(ApplicationInfo.Unit.cm, unitTexts[1]);
            buttons.Add(ApplicationInfo.Unit.m, unitTexts[2]);
        }
    }

    public void TestLog()
    {
        Debug.Log("Test");
    }

    public void OnClickUnitButton(int setUnit)
    {
        if (ApplicationInfo.Instance.unit != null)
        {
            buttons[ApplicationInfo.Instance.unit].color = Color.white;
        }
        ApplicationInfo.Instance.unit = (ApplicationInfo.Unit)setUnit;
        buttons[ApplicationInfo.Instance.unit].color = mainUIColor;

        ApplicationInfo.Instance.unitSet = true;
    }
}
