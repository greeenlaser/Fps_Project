using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask enemyLayer;

    [HideInInspector] public bool beenHit = false;

    [Header("Damage")]
    public int damage;

    // Hidden public variables
    [HideInInspector] public bool amLooking;

    [Header("Object References")]
    public GameObject crosshair;

    // Scripts
    UI_PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        bool raycaster = Physics.Raycast(transform.position,
                               transform.TransformDirection(Vector3.forward),
                               out hit,
                               Mathf.Infinity,
                               enemyLayer);

        if (raycaster)
        {
            amLooking = true;
        }
        else
        {
            amLooking = false;
        }

        if (Input.GetMouseButtonDown(0)
            && !pauseMenu.isPaused
            && raycaster)
        {
            Debug.Log("Hit");

            beenHit = true;
        }

        if (pauseMenu.isPaused)
        {
            crosshair.SetActive(false);
        }
        else
        {
            crosshair.SetActive(true);
        }
    }
}
