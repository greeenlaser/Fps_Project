using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Haelth")]
    public int health = 100;
    [SerializeField] TextMeshProUGUI healthText;

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
        shoot = FindObjectOfType<Shoot>();

        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        meshRenderer = GetComponent<MeshRenderer>();

        healthText.gameObject.SetActive(true);

        healthText.text = gameObject.name + "'s health: " + health;

        normalMat = meshRenderer.material;
    }

    public IEnumerator TakeDamage(int damage)
    {
        canTakeDamage = false;

        health -= damage;

        healthText.text = gameObject.name + "'s health: " + health;

        meshRenderer.material = hurtMat;

        yield return new WaitForSeconds(damageDelayTime);

        meshRenderer.material = normalMat;
    }

    void Update()
    {
        if (shoot.beenHit 
            && !canTakeDamage)
        {
            canTakeDamage = true;
            StartCoroutine(TakeDamage(shoot.damage));
            shoot.beenHit = false;
        }

        if(health <= 0)
        {
            Destroy(gameObject);

            healthText.gameObject.SetActive(false);
        }

        if (pauseMenu.isPaused)
        {
            healthText.gameObject.SetActive(false);
        }

        if(!pauseMenu.isPaused 
            && health > 0)
        {
            healthText.gameObject.SetActive(true);
        }
    }
}
