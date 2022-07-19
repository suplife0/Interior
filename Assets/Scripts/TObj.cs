using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TObj : MonoBehaviour
{
    public bool isEditing;

    public Vector3 scale;
    
    [SerializeField]
    private Transform scalingTransform;
    [SerializeField]
    private Transform ringTransform;
    [SerializeField]
    private Collider collider;
    
    public enum Unit
    {
        mm = 0,
        cm = 1,
        m = 2
    };

    public Unit unit;

    private void OnEnable()
    {
        
    }

    public void Initialize(Vector3 inputScale)
    {
        scale = inputScale;
        this.scalingTransform.localScale = scale;
        ringTransform.localScale = Vector3.one * 0.2f * Mathf.Max(inputScale.x, inputScale.z);
    }
    
    public void SetScale(Vector3 setScale)
    {
        scale = setScale;
        
        Debug.LogFormat("Set This Tracked Object Scale : {0}", scale);
        this.transform.localScale = scale;
    }

    public void PickUp()
    {
        isEditing = true;
        
        this.collider.enabled = false;
        this.scalingTransform.localPosition = new Vector3(0, 0.5f, 0);
    }

    public void PutDown()
    {
        isEditing = false;

        this.collider.enabled = true;
        this.scalingTransform.localPosition = Vector3.zero;
    }
}
