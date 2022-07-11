using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacingManager : MonoBehaviour
{
    public static PlacingManager Instance;
    
    public TObj currentTObj;
    public bool isEditing;
    [FormerlySerializedAs("planeDetector")] public RayDetector rayDetector;

    // Define One Frame For Coroutine
    private WaitForEndOfFrame oneFrame;

    private float prevMouseXPos;
    private float curMouseXPos;
    private float dragSensitivity = 0.1f;
    
    public void StartEdit(TObj tObj)
    {
        isEditing = true;

        if (currentTObj != null)
        {
            if (currentTObj.isEditing)
            {
                currentTObj.PutDown();
            }
        }
        currentTObj = tObj;
        currentTObj.PickUp();

        StartCoroutine(CoEdit());
    }

    IEnumerator CoEdit()
    {
        oneFrame = new WaitForEndOfFrame();
        while (isEditing)
        {
            if (rayDetector.IsPlane(out Vector3 planePoint))
            {
                currentTObj.transform.position = planePoint;
            }
            yield return oneFrame;
        }
    }

    public void FinishEdit()
    {
        currentTObj.PutDown();
        
        isEditing = false;
        currentTObj = null;
    }

    public void StopEdit()
    {
        if(currentTObj != null)
            currentTObj.PutDown();
        
        isEditing = false;
        currentTObj = null;
    }

    void RotateTObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMouseXPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            curMouseXPos = Input.mousePosition.x;
            currentTObj.transform.Rotate(0, (prevMouseXPos - curMouseXPos) * dragSensitivity, 0);
            prevMouseXPos = curMouseXPos;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if(isEditing)
            RotateTObj();
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(rayDetector.IsTObj(out TObj tObj))
                    StartEdit(tObj);
            }
        }
    }
}
