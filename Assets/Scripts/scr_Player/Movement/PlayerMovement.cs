using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float increasedSpeed;
    [SerializeField] private float normalSpeed;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Other assignables")]
    [SerializeField] private GameObject par_Managers;

    //private variables
    private bool isGrounded;
    private Vector3 move;
    private Vector3 velocity;
    private CharacterController controller;

    //scripts
    private UI_PauseMenu PauseMenu;
    private Manager_Console ConsoleScript;

    private void Awake()
    {
        PauseMenu = par_Managers.GetComponent<UI_PauseMenu>();
        ConsoleScript = par_Managers.GetComponent<Manager_Console>();

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 
                                         groundDistance, 
                                         groundMask);

        if (isGrounded 
            && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = increasedSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        controller.Move(speed * Time.deltaTime * move);

        if (isGrounded 
            && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
            ConsoleScript.CreateNewConsoleLine("Player jumped", "UNITY_LOG_MESSAGE");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
