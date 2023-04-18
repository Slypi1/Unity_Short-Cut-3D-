using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputSystem : MonoBehaviour, IPointerClickHandler
{
    public static Action<Transform> OnStartSlot;
    public static Action<Transform> OnStartFindPath;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)           
            OnStartSlot(gameObject.transform);      
        else if (eventData.button == PointerEventData.InputButton.Right)               
            OnStartFindPath(gameObject.transform);       
    }
}

  

