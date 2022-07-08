using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TrackingManager : MonoBehaviour
{
    public Transform trackingObj;
    public Transform scalingObj;
    public Transform ringObj;
    
    public Transform cameraTransform;

    public Text trackingText;
    public float dragSensitivity = 1.0f;
    
    bool isTracking = true;
    private bool isDetected = false;
    private bool isRotating = false;

    private float prevMouseXPos = 0;
    private float mouseXPos = 0;
    
    
    private RaycastHit planeHit;
    private RaycastHit objHit;
    private WaitForEndOfFrame oneFrame;
    private void Awake()
    {
        ApplicationInfo.Instance.SetUnit();
        
        scalingObj.localScale = ApplicationInfo.Instance.scale;
        ringObj.localScale = Vector3.one * 0.2f * Mathf.Max(scalingObj.localScale.x, scalingObj.localScale.z);
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
        trackingObj.position = planeHit.point;
    }

    private void UnDetectingPlane()
    {
        
    }

    private void StartDetectingPlane() 
    {
        //UIManager.Instance.trackingText.text = "Tracking";
        UIManager.Instance.trackingText.color = Color.white;
        UIManager.Instance.putButton.interactable = true;
        isDetected = true;
    }

    private void FinishDetectingPlane()
    {
        //UIManager.Instance.trackingText.text = "UnTracked";
        UIManager.Instance.trackingText.color = Color.gray;
        UIManager.Instance.putButton.interactable = false;
        isDetected = false;
    }

    public void PutDown()
    {
        isTracking = false;
        isRotating = false;

        scalingObj.localPosition = Vector3.zero;
        scalingObj.GetComponent<Collider>().enabled = true;
        ringObj.gameObject.SetActive(false);
    }

    public void PickUp()
    {
        isTracking = true;
        isRotating = true;

        scalingObj.localPosition = new Vector3(0, 0.5f, 0);
        scalingObj.GetComponent<Collider>().enabled = false;
        ringObj.gameObject.SetActive(true);
        
        StartTracking();
    }

    private bool GetPlaneRaycastHit()
    {
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.yellow);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out planeHit))
        {
            if (planeHit.transform.CompareTag("Plane"))
            {
                return true;
            }
        }
        return false;
    }

    private bool GetObjRaycastHit()
    {
        
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out objHit))
        {
            if (objHit.transform.CompareTag("Tracking"))
            {
                return true;
            }
        }
        return false;
    }
    
    private void Start()
    {
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

        if (!isTracking && Input.GetMouseButton(0))
        {
            if (GetObjRaycastHit())
            {
                PickUp();
            }
        }
    }
}
