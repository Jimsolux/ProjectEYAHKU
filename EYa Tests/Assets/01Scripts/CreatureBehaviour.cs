using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform transform2;
    [SerializeField] private Transform transform3;
    [SerializeField] private Transform transform4;
    public enum CurrentState
    {
        Idle,
        Bedroom,
        Meeting,
        Relicroom, Helping2, Helping3,
        Leaving
    }
    public CurrentState currentState;

    private void Awake()
    {
        //InvokeRepeating("GoToSecondLocation", 20, .05f);
    }

    private void FixedUpdate()
    {
        ActBehaviour();
    }

    private void ActBehaviour()
    {
        switch (currentState)
        {
            case CurrentState.Idle:
                break;
            case CurrentState.Bedroom:
                BedroomBehaviour();
                break;
            case CurrentState.Meeting:
                HallWayBehaviour2();
                break;
            case CurrentState.Relicroom:
                RelicroomBehaviour3();
                break;
            case CurrentState.Helping2: 
                break;
            case CurrentState.Helping3: 
                break;
            case CurrentState.Leaving:
                break;
        }
    }

    private IEnumerator waitaSec()
    {
        yield return new WaitForSeconds(5);
        GoToSecondLocation();
    }
    private void GoToSecondLocation()
    {
        Debug.Log("I should be moving to loc 2 now.");
        transform.position =  Vector3.MoveTowards(transform.position, transform2.position, .03f);
    }
    private void GoToThirdLocation()
    {
        Debug.Log("I should be moving to loc 3 now.");
        transform.position = Vector3.MoveTowards(transform.position, transform3.position, .03f);
    }

    private void GoToFourthLocation()
    {
        Debug.Log("I should be moving to loc 4 now.");
        transform.position = Vector3.MoveTowards(transform.position, transform4.position, .03f);
    }


    public void SwitchStateTo(int state)
    {
        currentState = (CurrentState)state;
    }
    //1
    bool hasPlayedAudio1;
    private void BedroomBehaviour()
    {
        if (!hasPlayedAudio1)
        {
            AudioCreature audio = GetComponentInChildren<AudioCreature>();
            audio.PlayTheAudio1();
        }
        GoToSecondLocation();
    }
    //2
    bool hasPlayedAudio2;
    private void HallWayBehaviour2()
    {
        if (!hasPlayedAudio2)
        {
            AudioCreature audio = GetComponentInChildren<AudioCreature>();
            audio.PlayTheAudio2();
        }
        GoToThirdLocation();
    }

    private void RelicroomBehaviour3()
    {
        GoToFourthLocation();
    }
}
