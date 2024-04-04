using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
   
    public UnityEvent onInteraction;


    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }


    public void Interact()
    {
        onInteraction.Invoke();
        
        //camera.fieldOfView = 110.0f;
    }


    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
