using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingManager : MonoBehaviour
{
    public Transform trackingObj;
    public Transform cameraTransform;

    public Text trackingText;
    public float dragSensitivity = 1.0f;
    
    bool isTracking = true;
    private bool isDetected = false;
    private bool isRotating = false;

    private float prevMouseXPos = 0;
    private float mouseXPos = 0;
    
    
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
        
        while (isTracking)
        {
            if (this.GetPlaneRaycastHit())
            {
                DetectingPlane();
                if (!isDetected)
                {
                    StartDetectingPlane();
                }
            }
            else
            {
                UnDetectingPlane();
                if (isDetected)
                {
                    FinishDetectingPlane();
                }
            }
            yield return oneFrame;
        }
    }

    private void DetectingPlane()
    {
        trackingObj.position = rayHit.point;
    }

    private void UnDetectingPlane()
    {
        
    }

    private void StartDetectingPlane()
    {
        UIManager.Instance.trackingText.text = "Tracking";
        UIManager.Instance.trackingText.color = Color.green;
        UIManager.Instance.putButton.interactable = true;
        isDetected = true;
    }

    private void FinishDetectingPlane()
    {
        UIManager.Instance.trackingText.text = "UnTracked";
        UIManager.Instance.trackingText.color = Color.red;
        UIManager.Instance.putButton.interactable = false;
        isDetected = false;
    }

    public void PutObject()
    {
        isTracking = false;
    }

    private bool GetPlaneRaycastHit()
    {
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.yellow);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out rayHit))
        {
            if (rayHit.transform.CompareTag("Plane"))
            {
                return true;
            }
        }
        return false;
    }
    
    private void Start()
    {
        if(ApplicationInfo.Instance != null)
            trackingObj.localScale = ApplicationInfo.Instance.scale / 10;
        StartTracking();
        
        isRotating = true;
    }

    private void Update()
    {
        if (isRotating)
        {
            if (Input.GetMouseButtonDown(0))
            {
                prevMouseXPos = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseXPos = Input.mousePosition.x;
                trackingObj.Rotate(0, (prevMouseXPos - mouseXPos) * dragSensitivity, 0);
                prevMouseXPos = mouseXPos;
            }
        }
    }
}
