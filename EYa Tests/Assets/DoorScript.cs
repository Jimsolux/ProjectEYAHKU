using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] int myCorrectKeyID;

    //player shit
    PlayerKeys playerKeys;
    Interactor myInteractor;

    private void Awake()
    {
        playerKeys = GameObject.Find("First Person Player").GetComponent<PlayerKeys>();
        myInteractor = playerKeys.GetComponent<Interactor>();
    }
    public void TryOpenDoor()
    {
        if(playerKeys.CheckKeyID() == myCorrectKeyID)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        //Destroy my current handobject.
        myInteractor.DeleteHeldItem();
        gameObject.SetActive(false);
    }
}
