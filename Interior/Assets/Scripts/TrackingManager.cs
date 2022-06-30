using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    public Transform trackingObj;
    public Transform cameraTransform;
    
    private RaycastHit rayHit;
    private WaitForEndOfFrame oneFrame;
    private void Awake()
    {
        
    }

    public void StartTracking()
    {
        oneFrame = new WaitForEndOfFrame();
        StartCoroutine(CoTracking());
    }

    private IEnumerator CoTracking()
    {
        bool isTracking = true;
        while (isTracking)
        {
            if (this.GetPlaneRaycastHit())
            {
                trackingObj.position = rayHit.transform.position;    
            }
            yield return oneFrame;
        }
    }

    private bool GetPlaneRaycastHit()
    {
        Debug.DrawRay(cameraTransform.position, transform.forward, Color.yellow);
        if(Physics.Raycast(cameraTransform.position, transform.forward, out rayHit))
        {
            if (rayHit.transform.CompareTag("Plane"))
            {
                Debug.Log("Hit");
                return true;
            }

            return false;
        }
        return false;
    }
    
    private void Start()
    {
        if(ApplicationInfo.Instance != null)
            trackingObj.localScale = ApplicationInfo.Instance.scale / 10;
        StartTracking();
    }
}
