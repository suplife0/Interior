using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationInfo : MonoBehaviour
{
    public static ApplicationInfo Instance;

    public Vector3 scale;


    public bool unitSet = false;
    
    public enum Unit
    {
        mm = 0,
        cm = 1,
        m = 2
    };

    public Unit unit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void SetUnit()
    {
        switch (unit)
        {
            case Unit.cm:
                scale = scale / 100;
                break;
            case Unit.m:
                break;
            case Unit.mm:
                scale = scale / 10000;
                break;
            default:
                break;
        }
    }
}
