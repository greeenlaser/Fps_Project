using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    Material normalMat;

    [SerializeField] Material hurtMat;

    [SerializeField] float damageDelayTime;

    MeshRenderer meshRenderer;

    public int health = 100;

    bool canTakeDamage;

    [SerializeField] TextMeshProUGUI healthText;

    void Start()
    { 
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
        if (Input.GetKeyDown(KeyCode.Tab) && !canTakeDamage)
        {
            canTakeDamage = true;
            StartCoroutine(TakeDamage(20)); 
        }

        if(health <= 0)
        {
            Destroy(gameObject);

            healthText.gameObject.SetActive(false);
        }
    }
}
