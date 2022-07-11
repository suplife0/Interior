using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class ScalingManager : MonoBehaviour
{
    public static ScalingManager Instance;
    
    public GameObject tObjPrefab;
    public Transform scalingCube;

    public Vector3 editingScale = Vector3.one;

    public Vector3 EditingScale
    {
        get { return editingScale; }
        set
        {
            editingScale = value;
            scalingCube.localScale = editingScale;
            SetCameraPosition();
        }
    }
    
    
    
    public enum Unit
    {
        mm,
        cm,
        m
    }

    public Unit unit;

    public Transform scalingCamera;

    private Vector3 cameraTargetPosition;
    private Vector3 initialPos = Vector3.zero;
    private WaitForEndOfFrame oneFrame;

    public void GenerateTObj(Vector3 scale)
    {
        GameObject tObj = Instantiate(tObjPrefab) as GameObject;
        tObj.transform.position = initialPos;
        
        tObj.GetComponent<TObj>().Initialize(scale);
        
        PlacingManager.Instance.StartEdit(tObj.GetComponent<TObj>());
    }
    
    public void GenerateTObj()
    {
        GameObject tObj = Instantiate(tObjPrefab) as GameObject;
        tObj.transform.position = initialPos;
        
        
        tObj.GetComponent<TObj>().Initialize(SetUnit(EditingScale));

        PlacingManager.Instance.StartEdit(tObj.GetComponent<TObj>());
    }

    private Vector3 SetUnit(Vector3 eScale)
    {
        switch (unit)
        {
            case Unit.mm:
                return eScale / 10000;
                break;
            case Unit.cm:
                return eScale / 100;
                break;
            case Unit.m:
                return eScale;
                break;
            default:
                break;
        }
        return Vector3.one;
    }
    public void XScaling(string inputString)
    {
        Debug.Log(ParseStringToInt(inputString));
        EditingScale = new Vector3(ParseStringToInt(inputString), EditingScale.y, EditingScale.z);
    }

    public void YScaling(string inputString)
    {
        EditingScale = new Vector3(EditingScale.x, ParseStringToInt(inputString), EditingScale.z);
    }

    public void ZScaling(string inputString)
    {
        EditingScale = new Vector3(EditingScale.x, EditingScale.y, ParseStringToInt(inputString));
    }
    
    private float ParseStringToInt(string inputString)
    {
        if (int.TryParse(inputString, out int outInt))
        {
            return outInt;
        }
        return 0;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    public void SetCameraPosition()
    {
        float maxAxis = Mathf.Max(EditingScale.x, EditingScale.y, EditingScale.z);

        cameraTargetPosition = new Vector3(0, 1, -2.5f) * maxAxis;

        oneFrame = new WaitForEndOfFrame();
        StartCoroutine(CoSetCameraPosition());
    }
    
    private IEnumerator CoSetCameraPosition()
    {
        bool moving = true;
        while (moving)
        {
            scalingCamera.position =
                Vector3.Lerp(scalingCamera.position, cameraTargetPosition, Time.deltaTime);
            
            // Stop Routine
            if (Mathf.Abs(cameraTargetPosition.y - scalingCamera.position.y) < 0.02f)
                moving = false;
            
            yield return oneFrame;
        }
    }
}
