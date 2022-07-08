using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPlaneDetector : MonoBehaviour
{
    public Transform cameraTransform;
    private RaycastHit planeHit;
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
}
