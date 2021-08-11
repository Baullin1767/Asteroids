using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    int pointerID;
    bool touched;

    private void Awake()
    {
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            touched = false;
        }
    }
    public bool Shoot() { return touched; }

}
