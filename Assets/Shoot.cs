using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public LayerMask enemyLayer;

    [HideInInspector] public bool beenHit = false;

    public int damage;

    UI_PauseMenu pauseMenu;

    public GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, enemyLayer))
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
