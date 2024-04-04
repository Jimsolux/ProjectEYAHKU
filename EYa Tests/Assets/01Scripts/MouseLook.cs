using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float mouseSens = 100f;


    public Transform playerBody;

    float xRotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;//Get MouseX Input
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;//Get MouseY Input

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);    // Dont look more than up or down.
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX); // X Rotation

        PlayerMovement.instance.camRotationX = Vector3.up * mouseX;
    }

    private void FixedUpdate()
    {
    }
}
