using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    // Hidden in inspector
    [HideInInspector] public int enemyCounter;

    [Header("List")]
    public GameObject[] enemies;

    [Header("References")]
    public Animator goalAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = enemies.Length;
    }

    void Update()
    {
        if (enemyCounter == 0)
        {
            goalAnim.SetTrigger("GoalDone");
        }
    }
}
