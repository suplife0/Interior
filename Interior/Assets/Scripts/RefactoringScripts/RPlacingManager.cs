using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPlacingManager : MonoBehaviour
{
    public TObj currentTObj;
    public bool isEditing;
    public RPlaneDetector planeDetector;


    private WaitForEndOfFrame oneFrame;
    
    void StartEdit(TObj tObj)
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
    }

    IEnumerator CoEdit()
    {
        oneFrame = new WaitForEndOfFrame();
        while (isEditing)
        {
            if (planeDetector.IsPlane(out Vector3 planePoint))
            {
                currentTObj.transform.position = planePoint;
            }
            
            yield return oneFrame;
        }
    }

    void StopEdit()
    {
        currentTObj.PutDown();
        
        isEditing = false;
        currentTObj = null;
    }

}
