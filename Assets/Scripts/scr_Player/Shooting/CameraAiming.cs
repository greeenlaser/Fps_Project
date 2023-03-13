using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAiming : MonoBehaviour
{
    // Private variables
    float normalfov;
    bool isAiming;
    Shoot shoot;
    Camera cam;
    CameraLook look;

    // Start is called before the first frame update
    void Awake()
    {
        shoot = GetComponent<Shoot>();

        cam = GetComponent<Camera>();

        look = GetComponent<CameraLook>();

        normalfov = cam.fieldOfView;

        isAiming = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView =  shoot.shootingfovAmount;

            if (!isAiming)
            {
                SensitivityUpChange();
            }
        }

        else
        {
            cam.fieldOfView = normalfov;

            if (isAiming)
            {
                SensitivityDownChange();
            }
        }
    }

    void SensitivityUpChange()
    {
        look.sensitivity *= .5f;

        isAiming = true;
    }

    void SensitivityDownChange()
    {
        look.sensitivity *= 2f;

        isAiming = false;
    }
}
