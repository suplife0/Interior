using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TObj : MonoBehaviour
{
    public bool isEditing;

    public Vector3 scale;
    public Transform scalingTransform;
    public Transform ringTransform;
    
    public enum Unit
    {
        mm = 0,
        cm = 1,
        m = 2
    };

    public Unit unit;

    public void SetScale(Vector3 setScale)
    {
        scale = setScale;
        
        Debug.LogFormat("Set This Tracked Object Scale : {0}", scale);
        this.transform.localScale = scale;
    }

    public void PickUp()
    {
        isEditing = true;
    }

    public void PutDown()
    {
        isEditing = false;
    }
}
