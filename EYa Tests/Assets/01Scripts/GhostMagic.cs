using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMagic : MonoBehaviour
{
    public GameObject currentHoldingObject;
    bool isHolding;
    public void HoldObject(GameObject obj)
    {
        if(isHolding)
        {
            ReleaseObject();
        }
        currentHoldingObject = obj;
        AddMagicObjectToObject(obj);
        //Get the rb and make kinematic
        Rigidbody rb = currentHoldingObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        isHolding = true;
    }


    public void ReleaseObject()
    {
        Debug.Log("Releasing object!");
        Rigidbody rb = currentHoldingObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        RemoveMagicObjectFromObject();
        isHolding = false;
    }

    [SerializeField] GameObject magicObject;
    private void AddMagicObjectToObject(GameObject obj)
    {
        if (magicObject != null && currentHoldingObject != null)
        {
            // Instantiate the object and set the parent
            GameObject newObject = Instantiate(magicObject, currentHoldingObject.transform);

            // Optionally set local position, rotation, and scale
            newObject.transform.localPosition = Vector3.zero;
            //newObject.transform.localRotation = Quaternion.identity;
            Vector3 originalScale = magicObject.transform.lossyScale;
            Vector3 parentScale = currentHoldingObject.transform.lossyScale;
            Vector3 newLocalScale = new Vector3(
                originalScale.x / parentScale.x,
                originalScale.y / parentScale.y,
                originalScale.z / parentScale.z
            );
            newObject.transform.localScale = newLocalScale;
        }
    }

    private void RemoveMagicObjectFromObject()
    {
        if(currentHoldingObject != null)
        {
            Debug.Log("RemovingMagicObject, I have a currentHoldingObject!");
            Transform childTransform = currentHoldingObject.transform.Find("PureMagick(Clone)");
            if (childTransform != null)
            {
                GameObject.Destroy(childTransform.gameObject);

                Debug.Log("Child found: " + childTransform.gameObject.name);
            }
        }
    }
}
