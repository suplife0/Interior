using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScalingManager : MonoBehaviour
{
    public Transform scalingCube;
    public Camera scalingCamera;
    public Vector3 cameraTargetPosition;

    public Toggle[] toggles;

    private WaitForEndOfFrame oneFrame;
    
    public void XScaling(string inputMsg)
    {
        if (int.TryParse(inputMsg, out int inputNum))
        {
            scalingCube.localScale =
                new Vector3(inputNum, scalingCube.localScale.y, scalingCube.localScale.z);
        }
        SetCameraPosition();
    }
    
    public void YScaling(string inputMsg)
    {
        if (int.TryParse(inputMsg, out int inputNum))
        {
            scalingCube.localScale =
                new Vector3(scalingCube.localScale.x, inputNum, scalingCube.localScale.z);
        }
        SetCameraPosition();
    }
    
    public void ZScaling(string inputMsg)
    {
        if (int.TryParse(inputMsg, out int inputNum))
        {
            scalingCube.localScale =
                new Vector3(scalingCube.localScale.x, scalingCube.localScale.y, inputNum);
        }
        SetCameraPosition();
    }

    public void SetCameraPosition()
    {
        float maxAxis = Mathf.Max(scalingCube.localScale.x, scalingCube.localScale.y, scalingCube.localScale.z);
        
        ApplicationInfo.Instance.scale = scalingCube.localScale;
        
        cameraTargetPosition = new Vector3(0, 1, -2.5f) * maxAxis;
        oneFrame = new WaitForEndOfFrame();
        StartCoroutine(CoSetCameraPosition());
    }

    private IEnumerator CoSetCameraPosition()
    {
        bool moving = true;
        while (moving)
        {
            scalingCamera.transform.position =
                Vector3.Lerp(scalingCamera.transform.position, cameraTargetPosition, Time.deltaTime);
            yield return oneFrame;
            if (scalingCamera.transform.position == cameraTargetPosition)
                moving = false;
        }
        StopAllCoroutines();
    }

    public void SetUnit()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                ApplicationInfo.Instance.unit = (ApplicationInfo.Unit) i;
            }
        }
    }
}
