using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int damage;
    public float speed;
    public int bitsOnDeath;
    void Start()
    {
        //Speed
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }

    void Update()
    {
        
    }
}
