using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Haelth")]
    public int health = 100;
    public Slider healthBar;

    [Header("Damage")]
    [SerializeField] float damageDelayTime;
    [SerializeField] Material hurtMat;

    // Private variables
    //bool canTakeDamage;
    MeshRenderer meshRenderer;
    Material normalMat;

    // Scripts
    Shoot shoot;
    UI_PauseMenu pauseMenu;
    EnemyCounter enemyCounter;

    void Awake()
    {
        healthBar.value = 100;

        healthBar.gameObject.SetActive(false);

        enemyCounter = FindObjectOfType<EnemyCounter>();

        shoot = FindObjectOfType<Shoot>();

        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        meshRenderer = GetComponent<MeshRenderer>();

        normalMat = meshRenderer.material;
    }

    public IEnumerator TakeDamage(int damage)
    {
        //canTakeDamage = false;

        health -= damage;

        healthBar.value = health;

        meshRenderer.material = hurtMat;

        yield return new WaitForSeconds(damageDelayTime);

        meshRenderer.material = normalMat;
    }

    void Update()
    {
        if(health <= 0)
        {
            enemyCounter.enemyCounter -= 1;

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
    }
}
