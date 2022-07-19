using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicScrollView : MonoBehaviour
{
    public RectTransform content;

    [Multiline(10)]
    public string textMessage;
    public TextMeshProUGUI tmp;

    private void Awake()
    {
        GetText();
    }

    public void GetText()
    {
        tmp.text = textMessage;
        
        Debug.Log(tmp.transform.localScale.x / tmp.pixelsPerUnit);
    }
}
