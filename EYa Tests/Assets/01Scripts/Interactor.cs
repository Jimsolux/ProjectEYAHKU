using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange = 50f;
    Interactable currentInteractable;

    //PickupSystem
    public GameObject itemHoldSlot;
    public GameObject itemCurrentlyHolding;
    public GameObject itemBeingSeen;
    bool isHolding = false;
    public float maxPickupDistance = 10f;
    //Crosshair
    public bool crossHairActive;

    void Update()
    {
        CheckInteraction();
        if (Input.GetKey(KeyCode.E) && currentInteractable != null)
        {
            Debug.Log("Should Check interaction");
            currentInteractable.Interact();
        }

        if (Input.GetKey(KeyCode.Z)) PickupItem();
        if (Input.GetKey(KeyCode.X)) DropItem();

        //if(itemBeingSeen != null)
        //{
        //    CrossHairColour.instance.SetCrossHairColour(itemBeingSeen);
        //}
    }//end update


    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(InteractorSource.transform.position, InteractorSource.transform.forward);   // Creates new Ray forwards from Camera
        Debug.DrawRay(InteractorSource.transform.position, InteractorSource.transform.forward * 10f, Color.green, 3.0f);
        Debug.Log("Ray has been shot");
        if (Physics.Raycast(ray, out hit, InteractRange))  // if Ray hits something in InteractRange (hit = true) , out hitInfo
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();   // If interactable object is hit, 
                Debug.Log("Sees Object." + hit.collider.gameObject.name);
                // If there is a currentInteractable and it is not the newInteractable, overlapping items,
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }

                else //If new interactable is not enabled.
                {
                    DisableCurrentInteractable();
                }
            }
            if (hit.collider.tag == "Punchable")
            {
                PlayerMovement.instance.SetPunchableObject(hit.collider.gameObject);
                Debug.Log("Set hittableObject to" + hit.collider.gameObject.name);
            }
            if (hit.collider.tag == "Item")
            {
                itemBeingSeen = hit.collider.gameObject;
                PlayerMovement.instance.SetPunchableObject(hit.collider.gameObject);
                Debug.Log("Set hittableObject to" + hit.collider.gameObject.name);
                EnableCrossHairActive();
            }
            else //If object is not interactable.
            {
                DisableCurrentInteractable();
                PlayerMovement.instance.hittableObject = null;
                itemBeingSeen = null;
            }
        }// end raycast

        else //If nothing within reach.
        {
            DisableCurrentInteractable();
            PlayerMovement.instance.hittableObject = null;
            itemBeingSeen = null;
        }

    }// end Checkinteraction()

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        EnableCrossHairActive();
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
            DisableCrossHairActive();
        }
    }

    public void EnableCrossHairActive()
    {
        CrossHairColour.instance.SetCrossHairColour(true);
        crossHairActive = true;
    }

    public void DisableCrossHairActive()
    {
        CrossHairColour.instance.SetCrossHairColour(false);
        crossHairActive = false;
    }

    #region Pickups
    void PickupItem()
    {
        if (itemBeingSeen != null)
        {
            Debug.Log("Sees pickable item.");
            if (isHolding) { DropItem(); }   // Drops item already holding.

            itemCurrentlyHolding = itemBeingSeen.transform.gameObject;
            foreach (var item in itemBeingSeen.transform.GetComponentsInChildren<Collider>()) if (item != null) { item.enabled = false; }
            foreach (var rb in itemBeingSeen.transform.GetComponentsInChildren<Rigidbody>()) if (rb != null) { rb.isKinematic = true; }

            itemCurrentlyHolding.transform.parent = itemHoldSlot.transform;  // Item becomes child of playerobject
            itemCurrentlyHolding.transform.localPosition = Vector3.zero;    // sets localPosition to 0
            itemCurrentlyHolding.transform.localEulerAngles = Vector3.zero;

            isHolding = true;
        }
        else { Debug.Log("attempted to pickup item but there wasnt any items seen."); }
    }

    void DropItem()
    {
        if (isHolding)
        {
            itemCurrentlyHolding.transform.parent = null;
            foreach (var item in itemCurrentlyHolding.GetComponentsInChildren<Collider>()) if (item != null) { item.enabled = true; }
            foreach (var rb in itemCurrentlyHolding.GetComponentsInChildren<Rigidbody>()) if (rb != null) { rb.isKinematic = false; }
            isHolding = false;
            RaycastHit hitDown; //floor for dropping
            Physics.Raycast(itemHoldSlot.transform.position, -Vector3.up, out hitDown);

            itemCurrentlyHolding.transform.position = hitDown.point + new Vector3(transform.forward.x, 0, transform.forward.z);
            itemCurrentlyHolding = null;
        }
    }

    #endregion
}


