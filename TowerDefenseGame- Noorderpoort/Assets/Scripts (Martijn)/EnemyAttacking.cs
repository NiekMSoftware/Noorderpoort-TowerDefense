using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAttacking : MonoBehaviour
{
    private bool MTInRange = false;
    private bool TInRange = false;
    private bool CanAttack = true;

    public float enemyCoolDown = 2;
    public float damage = 10;
    //hoe veel een enemy damage doet
    public float attackRate = 3.5f;     
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {

        }
        if (other.gameObject.CompareTag("Finish"))
        {
            MTInRange = true;
        }
    }
    private void Update()
    {
        if (MTInRange && CanAttack)
        {
            GameObject.Find("Finish").GetComponent<MainTowerHealth>().MTHealth -= damage;
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