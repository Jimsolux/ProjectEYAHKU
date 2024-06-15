using System.Collections;
using UnityEngine;

public class CloseSelf10Seconds : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CloseAfter10s());
    }

    private IEnumerator CloseAfter10s()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
