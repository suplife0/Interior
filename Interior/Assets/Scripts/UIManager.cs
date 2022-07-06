using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public Text debugText;

    public Text trackingText;
    public Button putButton;
    
    
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void TestLog()
    {
        Debug.Log("Test");
    }
}
