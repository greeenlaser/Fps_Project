using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Haelth")]
    public int health = 100;
    [SerializeField] Slider healthBar;

    [Header("Damage")]
    [SerializeField] float damageDelayTime;
    [SerializeField] Material hurtMat;

    // Private variables
    bool canTakeDamage;
    MeshRenderer meshRenderer;
    Material normalMat;

    // Scripts
    Shoot shoot;
    UI_PauseMenu pauseMenu;

    void Awake()
    {
        healthBar.value = 100;

        healthBar.gameObject.SetActive(false);

        shoot = FindObjectOfType<Shoot>();

        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        meshRenderer = GetComponent<MeshRenderer>();

        normalMat = meshRenderer.material;
    }

    public IEnumerator TakeDamage(int damage)
    {
        canTakeDamage = false;

        health -= damage;

        healthBar.value = health;

        meshRenderer.material = hurtMat;

        yield return new WaitForSeconds(damageDelayTime);

        meshRenderer.material = normalMat;
    }

    void Update()
    {
        if (shoot.damageTaken 
            && !canTakeDamage)
        {
            canTakeDamage = true;
            StartCoroutine(TakeDamage(shoot.damage));
            shoot.beenHit = false;
            shoot.damageTaken = false;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        // health bar with pause menu
        if (pauseMenu.isPaused)
        {
            healthBar.gameObject.SetActive(false);
        }

        if(!pauseMenu.isPaused 
            && health > 0)
        {
            healthBar.gameObject.SetActive(true);
        }

        // health bar when looking at it
        if (shoot.amLooking)
        {
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}
