using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    public Transform trackingObj;
    
    private RaycastHit rayHit;
    private void Awake()
    {
        
    }

    private void Start()
    {
        trackingObj.localScale = ApplicationInfo.Instance.scale / 10;
    }
}
