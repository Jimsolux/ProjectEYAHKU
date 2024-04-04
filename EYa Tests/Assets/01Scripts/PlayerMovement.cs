using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public static PlayerMovement instance;

    public float speed = 12f;
    public float gravity = 9.81f;
    public float jumpHeight = 3;

    Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Camera Rotation
    public Vector3 camRotationX;
    //Punching
    public GameObject hittableObject = null;
    public float punchStrength;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)   // Check fallSpeed
        {
            velocity.y = 0;
        }
        // MOVEMENT
        float x = Input.GetAxis("Horizontal"); // Sidewards movement.
        float z = Input.GetAxis("Vertical"); // Forwards movement.

        Vector3 move = transform.right * x + transform.forward * z; //MOTOR MOVE - - - FIRST Rotation, Then Forward speed * Z axis.

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            Debug.Log("ATTEMPTING TO JUMP");
        }

        velocity.y -= gravity * 2 * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q)) PunchObject(); // punch on Q
    }

    public void PunchObject()
    {
        if(hittableObject != null)
        {
            hittableObject.GetComponent<Rigidbody>().AddForce(transform.forward * punchStrength);
            hittableObject.GetComponent<Rigidbody>().AddForce(camRotationX * punchStrength);
        }
    }

    public void SetPunchableObject(GameObject punchThing)
    {
        if(hittableObject != punchThing) 
        { 
        Debug.Log(punchThing.name + " has been set as object in player");
        hittableObject = punchThing;
        }
    }

    

}
