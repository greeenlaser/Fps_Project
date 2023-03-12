using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask enemyLayer;

    // Hidden in inspector
    [HideInInspector] public bool hasShot = false;
    [HideInInspector] public float shootingfovAmount;

    [Header("Guns")]
    public GameObject pistol;
    public GameObject assaultRifle;

    //ammo
    int ammoValue;

    [Header("Shooting")]
    public float shootDelay;
    public int damage;
    [SerializeField] float maxShootDistance;

    // Hidden public variables
    [HideInInspector] public bool amLooking;

    // Recoil
    float minDistanceForRecoil;
    float maxDistanceForRecoil;

    [Header("Object References")]
    public GameObject crosshair;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunTypeText;

    // Scripts
    UI_PauseMenu pauseMenu;

    // Gun data
    [SerializeField] ShootSlot[] shootSlot;

    // Audio
    AudioSource audioSource;
    AudioClip gunSound;

    //Private variables
    bool canShoot;
    int gunValue;
    bool raycaster;
    RaycastHit hit;
    static float t = 0.0f;

    [System.Serializable]
    class ShootSlot
    {
        public GunType gun;

        public int ammoAmount;

        public int damage;

        public float delay;

        public float aimValue;

        public float shootingDistance;

        public AudioClip shootSound;

        public float minRecoilDist;

        public float maxRecoilDist;
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        ammoText.gameObject.SetActive(false);
        gunTypeText.gameObject.SetActive(false);

        canShoot = true;

        Pistol();
    }

    // Update is called once per frame
    void Update()
    {
        raycaster = Physics.Raycast(transform.position,
                               transform.TransformDirection(Vector3.forward),
                               out hit,
                               Mathf.Infinity,
                               enemyLayer);

        StartCoroutine(ShootBullet());

        if (raycaster)
        {
            hit.transform.gameObject
                .GetComponent<EnemyHealth>()
                .healthBar
                .gameObject
                .SetActive(true);
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

        SetMaxShootingDistance();

        EnableWeapons();

        Debug.Log(hit.distance);
    }

    IEnumerator ShootBullet()
    {
        if (Input.GetMouseButtonDown(0)
            && !pauseMenu.isPaused
            && raycaster
            && ammoValue > 0
            && canShoot
            && hit.distance <= maxShootDistance
            )
        {
            canShoot = false;

            ammoValue -= 1;

            hit.transform.gameObject.
                GetComponent<EnemyHealth>().
                StartCoroutine(hit
                                .transform
                                .gameObject
                                .GetComponent<EnemyHealth>()
                                .TakeDamage(damage));

            yield return new WaitForSeconds(shootDelay);

            canShoot = true;
        }

        if (Input.GetMouseButtonDown(0)
            && ammoValue > 0
            && canShoot
            &&!pauseMenu.isPaused)
        {
            audioSource.PlayOneShot(gunSound);

            canShoot = false;

            hasShot = true;

            TakeAmmo();

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
            Pistol();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AssaultRifle();
        }
    }

    void TakeAmmo()
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

    void SetMaxShootingDistance()
    {
        // To set max sho

        if (gunValue == 0)
        {
            maxShootDistance = shootSlot[0].shootingDistance;
        }

        if (gunValue == 1)
        {
            maxShootDistance = shootSlot[1].shootingDistance;
        }
    }

    void EnableWeapons()
    {
        if (gunValue == 0)
        {
            pistol.SetActive(true);
        }
        else
        {
            pistol.SetActive(false);
        }

        if (gunValue == 1)
        {
            assaultRifle.SetActive(true);
        }
        else
        {
            assaultRifle.SetActive(false);
        }
    }

    // Weapon functions go below here
    // For setting the variables 

    void Pistol()
    {
        gunValue = 0;

        ammoText.gameObject.SetActive(true);
        gunTypeText.gameObject.SetActive(true);

        gunTypeText.text = shootSlot[0].gun.ToString();

        ammoValue = shootSlot[0].ammoAmount;

        damage = shootSlot[0].damage;

        shootDelay = shootSlot[0].delay;

        shootingfovAmount = shootSlot[0].aimValue;

        maxShootDistance = shootSlot[0].shootingDistance;

        gunSound = shootSlot[0].shootSound;

        minDistanceForRecoil = shootSlot[0].minRecoilDist;

        maxDistanceForRecoil = shootSlot[0].maxRecoilDist;
    }

    void AssaultRifle()
    {
        gunValue = 1;

        ammoText.gameObject.SetActive(true);
        gunTypeText.gameObject.SetActive(true);

        gunTypeText.text = shootSlot[1].gun.ToString();

        ammoValue = shootSlot[1].ammoAmount;

        damage = shootSlot[1].damage;

        shootDelay = shootSlot[1].delay;

        shootingfovAmount = shootSlot[1].aimValue;

        maxShootDistance = shootSlot[1].shootingDistance;

        gunSound = shootSlot[1].shootSound;

        minDistanceForRecoil = shootSlot[1].minRecoilDist;

        maxDistanceForRecoil = shootSlot[1].maxRecoilDist; 
    }
}
