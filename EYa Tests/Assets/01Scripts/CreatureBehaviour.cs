using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform transform2;
    [SerializeField] private Transform transform3;
    [SerializeField] private Transform transform4;
    enum CurrentState
    {
        Spooking,
        Meeting,
        Helping1, Helping2, Helping3,
        Leaving
    }
    CurrentState behaviourNow;

    private void Awake()
    {
        InvokeRepeating("GoToSecondLocation", 10, .05f);
    }

    private void FixedUpdate()
    {

    }

    private void ActBehaviour()
    {
        switch (behaviourNow)
        {
            case CurrentState.Spooking:
                break;
            case CurrentState.Meeting:
                break;
            case CurrentState.Helping1:
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

}
