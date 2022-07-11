using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetector : MonoBehaviour
{
    public Transform cameraTransform;
    private RaycastHit planeHit;
    private RaycastHit objHit;
    private Vector3 planePoint;
    
    public bool IsPlane(out Vector3 planePoint)
    {
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.yellow);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out planeHit))
        {
            if (planeHit.transform.CompareTag("Plane"))
            {
                planePoint = planeHit.point;
                return true;
            }
        }
        planePoint = Vector3.zero;
        return false;
    }

    public bool IsTObj(out TObj tObj)
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out objHit))
        {
            if (objHit.transform.CompareTag("Tracking"))
            {
                tObj = objHit.transform.parent.GetComponent<TObj>();
                return true;
            }
        }

        tObj = null;
        return false;
    }
}
