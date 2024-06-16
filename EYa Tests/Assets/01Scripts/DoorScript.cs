using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] int myCorrectKeyID;

    //player shit
    PlayerKeys playerKeys;
    Interactor myInteractor;
    //Ui stuff
    [SerializeField] GameObject closedDoorUI;
    [SerializeField] GameObject closedDoorUI2;


    private void Awake()
    {
        playerKeys = GameObject.Find("First Person Player").GetComponent<PlayerKeys>();
        myInteractor = playerKeys.GetComponent<Interactor>();
    }
    public void TryOpenDoor()
    {
        if(playerKeys.CheckKeyID() == myCorrectKeyID)//correct key
        {
            OpenDoor();
        }
        else if (playerKeys.CheckKeyID() != myCorrectKeyID && playerKeys.CheckKeyID() != -1)//incorrect key, but i have a key.
        {
            closedDoorUI2.SetActive(true);
            CloseSelf10Seconds cs102 = closedDoorUI2.GetComponent<CloseSelf10Seconds>();
            cs102.StartTheCloseCountdown();
        }
        else if (playerKeys.CheckKeyID() == -1)//i dont have a key.
        {
            //display msg, wrong key
            closedDoorUI.SetActive(true);
            CloseSelf10Seconds cs10 = closedDoorUI.GetComponent<CloseSelf10Seconds>();
            cs10.StartTheCloseCountdown();
        }
    }

    private void OpenDoor()
    {
        //Destroy my current handobject.
        myInteractor.DeleteHeldItem();
        gameObject.SetActive(false);
    }
}
