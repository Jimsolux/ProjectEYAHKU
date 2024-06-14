using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    private Dictionary<int, GameObject> keyDictionary = new Dictionary<int, GameObject>();

    private int keyIdCounter = 0;


    Interactor myInteractor;
    // interactor.currentInteractable.id???
    private void Awake()
    {
        myInteractor = GetComponent<Interactor>();
    }

    public int CheckKeyID()
    {
        if (myInteractor.currentInteractableID != -1)
        {
            return myInteractor.currentInteractableID;
        }
        else
        {
            return -1;
        }
    }


    public int AddKey(GameObject key)
    {
        int uniqueID = keyIdCounter++; //Creates a unique ID for new key.
        keyDictionary.Add(uniqueID, key); //Adds the key + uniqueID to dictionary
        return uniqueID;
    }

    public GameObject GetKeyById(int id)
    {
        keyDictionary.TryGetValue(id, out GameObject keyObj);   //looks up key by ID.
        return keyObj;
    }

    public bool HasGameObject(int id)
    {
        return keyDictionary.ContainsKey(id);
    }

    public bool RemoveKey(int id)
    {
        return keyDictionary.Remove(id);
    }
}
