using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAttacking : MonoBehaviour
{
    public bool MTInRange = false;
    //private bool TInRange = false;
    private bool CanAttack = true;
    public float attackRate = 3.5f;     
    private EnemyStats stats;
/*    public MainTowerHealth mainTowerHealth;*/
    public Healthscript mainTowerHealth;
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            MTInRange = true;
        }
    }
    private void Update()
    {
        if (MTInRange && CanAttack)
        {
            mainTowerHealth.HealthIndex -= stats.damage;

            GetComponent<EnemyHP>().TakeDamage(9999999999999999);
        }
    }
}