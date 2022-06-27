using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationInfo : MonoBehaviour
{
    public static ApplicationInfo Instance;

    public Vector3 scale;
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
        }
    }
}
