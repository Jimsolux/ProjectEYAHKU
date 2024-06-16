using System.Collections;
using UnityEngine;

public class CloseSelf10Seconds : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CloseAfter10s());
    }

    public void StartTheCloseCountdown()
    {
        StartCoroutine(CloseAfter10s());
    }

    private IEnumerator CloseAfter10s()
    {
        Debug.Log("Should close in 4 secs");
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }
}
