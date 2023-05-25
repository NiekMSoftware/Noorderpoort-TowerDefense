using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public int damage;
    public float speed;
    public int bitsOnDeath;
    void Start()
    {
        //Health Multiplier
        health = health * FindObjectOfType<WaveSystem>().enemyHealthMultiplier;
        //Speed
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }

    void Update()
    {
        
    }
}
