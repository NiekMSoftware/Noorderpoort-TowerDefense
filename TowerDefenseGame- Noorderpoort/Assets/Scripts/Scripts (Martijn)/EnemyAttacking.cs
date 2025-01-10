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

    public float enemyCoolDown = 2;
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
        if (other.gameObject.CompareTag("Tower"))
        {
            //TInRange = true;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            print("mtinrange");
            MTInRange = true;
        }
    }
    private void Update()
    {
/*        if (TInRange && CanAttack)
        {
            GameObject.Find("Tower").GetComponent<EnemyAttacking>();
        }*/
        if (MTInRange && CanAttack)
        {
            mainTowerHealth.HealthIndex -= stats.damage;

            GetComponent<EnemyHP>().takeDamage(9999999999999999);
            
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        CanAttack = false;
        yield return new WaitForSeconds(enemyCoolDown);
        CanAttack = true;
    }
}