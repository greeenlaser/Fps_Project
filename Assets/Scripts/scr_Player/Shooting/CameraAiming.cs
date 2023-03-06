using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAiming : MonoBehaviour
{
    Shoot shoot;

    Camera cam;

    float normalfov;

    // Start is called before the first frame update
    void Start()
    {
        shoot = GetComponent<Shoot>();

        cam = GetComponent<Camera>();

        normalfov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView =  shoot.shootingfovAmount;
        }
        else
        {
            cam.fieldOfView = normalfov;
        }
    }
}
