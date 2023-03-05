using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask enemyLayer;

    // Hidden in inspector
     public bool beenHit = false;
     public bool damageTaken = false;
    public bool hasShot = false;

    //ammo
    int ammoValue;

    [Header("Shooting")]
    public float shootDelay;
    public int damage;

    // Hidden public variables
    [HideInInspector] public bool amLooking;


    [Header("Object References")]
    public GameObject crosshair;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunTypeText;

    // Scripts
    UI_PauseMenu pauseMenu;

    // Gun data
    [SerializeField] ShootSlot[] shootSlot;

    //Private variables
    bool canShoot;
    int gunValue;
    bool raycaster;

    [System.Serializable]
    class ShootSlot
    {
        public GunType gun;

        public int ammoAmount;

        public int damage;

        public float delay;
    }

    // Start is called before the first frame update
    void Awake()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        ammoText.gameObject.SetActive(false);
        gunTypeText.gameObject.SetActive(false);

        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        raycaster = Physics.Raycast(transform.position,
                               transform.TransformDirection(Vector3.forward),
                               out hit,
                               Mathf.Infinity,
                               enemyLayer);

        StartCoroutine(ShootBullet());

        if (raycaster)
        {
            amLooking = true;
        }
        else
        {
            amLooking = false;
        }

        ammoText.text = ammoValue.ToString();

        if (pauseMenu.isPaused)
        {
            crosshair.SetActive(false);
            ammoText.gameObject.SetActive(false);
            gunTypeText.gameObject.SetActive(false);
        }
        else
        {
            crosshair.SetActive(true);
            ammoText.gameObject.SetActive(true);
            gunTypeText.gameObject.SetActive(true); 
        }

        GunSwapping();
    }

    IEnumerator ShootBullet()
    {
        if (Input.GetMouseButtonDown(0)
            && !pauseMenu.isPaused
            && raycaster
            && ammoValue > 0
            && canShoot)
        {
            canShoot = false;

            beenHit = true;

            ammoValue -= 1;

            yield return new WaitForSeconds(shootDelay);

            canShoot = true;
        }

        if (beenHit)
        {
            damageTaken = true;
        }

        if (Input.GetMouseButtonDown(0)
            && ammoValue > 0
            && canShoot
            &&!pauseMenu.isPaused)
        {
            canShoot = false;

            hasShot = true;

            CheckForGunValue();

            ammoValue -= 1;

            yield return new WaitForSeconds(shootDelay);

            hasShot = false;

            canShoot = true;
        }
    }


    public void GunSwapping()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunValue = 0;

            ammoText.gameObject.SetActive(true);
            gunTypeText.gameObject.SetActive(true);

            gunTypeText.text = shootSlot[0].gun.ToString();

            ammoValue = shootSlot[0].ammoAmount;

            damage = shootSlot[0].damage;

            shootDelay = shootSlot[0].delay;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunValue = 1;

            ammoText.gameObject.SetActive(true);
            gunTypeText.gameObject.SetActive(true);

            gunTypeText.text = shootSlot[1].gun.ToString();

            ammoValue = shootSlot[1].ammoAmount;            

            damage = shootSlot[1].damage;

            shootDelay = shootSlot[1].delay;
        }
    }

    void CheckForGunValue()
    {
        if (hasShot 
            && gunValue == 0)
        {
            shootSlot[0].ammoAmount -= 1;
        }

        if (hasShot 
            && gunValue == 1)
        {
            shootSlot[1].ammoAmount -= 1;
        }
    }
}
