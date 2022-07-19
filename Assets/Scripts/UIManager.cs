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
    
    public Animation scalingPanelAnim;
    
    private Dictionary<ScalingManager.Unit, Text> buttons;

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
            buttons = new Dictionary<ScalingManager.Unit, Text>(3);
            buttons.Add(ScalingManager.Unit.mm, unitTexts[0]);
            buttons.Add(ScalingManager.Unit.cm, unitTexts[1]);
            buttons.Add(ScalingManager.Unit.m, unitTexts[2]);
        }
    }

    public void TestLog()
    {
        Debug.Log("Test");
    }

    public void OnClickUnitButton(int setUnit)
    {
        buttons[ScalingManager.Instance.unit].color = Color.black;    
        ScalingManager.Instance.unit = (ScalingManager.Unit)setUnit;
        buttons[ScalingManager.Instance.unit].color = mainUIColor;
    }

    public void OnClickAddButton()
    {
        Debug.Log("Hellos");
        scalingPanelAnim["ScalingPanelAnimation"].speed = 1;
        scalingPanelAnim["ScalingPanelAnimation"].time = 0;
        scalingPanelAnim.Play();
    }

    public void OnClickCloseButton()
    {
        scalingPanelAnim["ScalingPanelAnimation"].speed = -1;
        scalingPanelAnim["ScalingPanelAnimation"].time = scalingPanelAnim["ScalingPanelAnimation"].length;
        scalingPanelAnim.Play();
    }
}
