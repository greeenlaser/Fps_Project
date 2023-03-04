using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement

    CharacterController controller;

    Vector3 move;

    public float speed = 5f;

    public float increasedSpeed;
    public float normalSpeed;

    //Gravity + physics

    Vector3 velocity;

    public float gravity = -9.81f;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float groundDistance = 0.4f;

    bool isGrounded;

    public float jumpHeight;

    UI_PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
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

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
