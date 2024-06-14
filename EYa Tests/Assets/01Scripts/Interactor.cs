using Unity.VisualScripting;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange = 5f;
    Interactable currentInteractable;
    public int currentInteractableID = -1;
    private bool canInteractCusIHaveInteractable;

    //PickupSystem
    public GameObject itemHoldSlot;
    public GameObject itemCurrentlyHolding;
    public GameObject itemBeingSeen;
    bool isHolding = false;
    public float maxPickupDistance = 10f;
    //Crosshair
    [SerializeField] CrossHairColour crosshairColour;
    public bool crossHairActive;

    void Update()
    {
        CheckInteraction();
        if (Input.GetKey(KeyCode.E) && canInteractCusIHaveInteractable)
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
        //Debug.Log("Ray has been shot");
        if (Physics.Raycast(ray, out hit, InteractRange))  // if Ray hits something in InteractRange (hit = true) , out hitInfo
        {
            if (hit.collider.tag == "Interactable")
            {
                Debug.Log("Sees Object." + hit.collider.gameObject.name);
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();   // If interactable object is hit, 
                // If there is a currentInteractable and it is not the newInteractable, overlapping items,
                //if (currentInteractable && newInteractable != currentInteractable)
                //{
                //    currentInteractable.DisableOutline();
                //}
                if (newInteractable != null && newInteractable != currentInteractable)
                {
                    SetNewCurrentInteractable(newInteractable);
                    Debug.Log("SetCurrentInteractable");
                }
            else //If object is not interactable.
            {
                DisableCurrentInteractable();
                PlayerMovement.instance.hittableObject = null;
                itemBeingSeen = null;
                Debug.Log("Disabled cus the object is not interactable.");

            }

                //else //If new interactable is not enabled.
                //{
                //    DisableCurrentInteractable();
                //    Debug.Log("Disabled cus new interactable was null");
                //}
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
        }// end raycast

        else //If nothing within reach.
        {
            DisableCurrentInteractable();
            PlayerMovement.instance.hittableObject = null;
            itemBeingSeen = null;
            Debug.Log("Disabled cus I think I see no item.");

        }

    }// end Checkinteraction()

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        EnableCrossHairActive();
        Debug.Log("Actually set the new currentinteractable");
        if (itemCurrentlyHolding != null)
        {
            currentInteractableID = itemCurrentlyHolding.GetComponent<ObjectID>().myID;
            Debug.Log("I took my ID from my item holding.");

        }
        canInteractCusIHaveInteractable = true;
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
            currentInteractableID = -1;
        }
        canInteractCusIHaveInteractable = false;
        DisableCrossHairActive();
    }

    public void EnableCrossHairActive()
    {
        crosshairColour.SetCrossHairColour(true);
        crossHairActive = true;
    }

    public void DisableCrossHairActive()
    {
        crosshairColour.SetCrossHairColour(false);
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

    [SerializeField] LayerMask dropRayCastMask;
    void DropItem()
    {
        if (isHolding)
        {
            itemCurrentlyHolding.transform.parent = null;
            foreach (var item in itemCurrentlyHolding.GetComponentsInChildren<Collider>()) if (item != null) { item.enabled = true; }
            foreach (var rb in itemCurrentlyHolding.GetComponentsInChildren<Rigidbody>()) if (rb != null) { rb.isKinematic = false; }
            isHolding = false;
            RaycastHit hitDown; //floor for dropping
            if (Physics.Raycast(itemHoldSlot.transform.position, InteractorSource.forward, out hitDown, InteractRange))
            {

            }
            else
            {
                Physics.Raycast(itemHoldSlot.transform.position, -Vector3.up, out hitDown);
            }

            itemCurrentlyHolding.transform.position = hitDown.point; //+ new Vector3(transform.forward.x, 0, transform.forward.z);
            itemCurrentlyHolding = null;
        }
    }

    public void DeleteHeldItem()
    {
        if(isHolding)
        {
            GameObject.Destroy(itemCurrentlyHolding);
            isHolding = false;
            itemCurrentlyHolding = null;
        }
    }
    #endregion
}


