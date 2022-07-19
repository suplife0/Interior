using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputInterfaceSample : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Pointer Up");
    }

    public void OnClick()
    {
        Debug.Log("Click");
    }

    public void OnHover()
    {
        Debug.Log("Hover");
    }
}
