using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Settings : MonoBehaviour
{
    [Header("UI")]
    //camera speed
    [SerializeField] private Slider cameraSpeedSlider;
    [SerializeField] private TMP_Text txt_CameraSpeedSliderValue;

    [Header("Scripts")]
    [SerializeField] private CameraLook PlayerCamera;

    //assign player camera speed
    public void CameraSpeed()
    {
        PlayerCamera.sensitivity = cameraSpeedSlider.value;
        txt_CameraSpeedSliderValue.text = cameraSpeedSlider.value.ToString();
    }
}