using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Assignables")]
    [SerializeField] private GameObject par_Managers;

    [Header("Other Variables")]
    public int level;

    //private variables
    private bool isGrounded;
    private Vector3 move;
    private Vector3 velocity;
    private CharacterController controller;

    // Hidden variables
    [HideInInspector] public int bulletType;

    //scripts
    private UI_PauseMenu PauseMenu;
    private Manager_Console ConsoleScript;
    Shoot shoot;

    private void Awake()
    {
        PauseMenu = par_Managers.GetComponent<UI_PauseMenu>();
        ConsoleScript = par_Managers.GetComponent<Manager_Console>();

        controller = GetComponent<CharacterController>();

        level = SceneManager.GetActiveScene().buildIndex;

        shoot = GetComponentInChildren<Shoot>();
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

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Goal"))
        {
            if (level == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (level == 2)
            {
                SceneManager.LoadScene(0);

                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;
            }
        }

        if (col.CompareTag("Pistol Bullet"))
        {
            Debug.Log("touching " + col.tag);

            shoot.shootSlot[0].ammoAmount += shoot.shootSlot[0].bulletPickUpAmount;

            if (shoot.gunValue == 0)
            {
                shoot.ammoValue = shoot.shootSlot[0].ammoAmount;
            }

            Destroy(col.gameObject);
        }

        if (col.CompareTag("AR Bullet"))
        {
            Debug.Log("touching " + col.tag);

            shoot.shootSlot[1].ammoAmount += shoot.shootSlot[1].bulletPickUpAmount;

            if (shoot.gunValue == 1)
            {
                shoot.ammoValue = shoot.shootSlot[1].ammoAmount;
            }

            Destroy(col.gameObject);
        }
    }
}
