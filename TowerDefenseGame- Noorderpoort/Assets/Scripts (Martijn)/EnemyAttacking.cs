using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAttacking : MonoBehaviour
{
    public int damage = 10;
    public int health = 100;
    public float speed = 5;
    public float attackRate = 3;
    EnemyRangeScript MyEnemyInRage;
    MainTowerHealth MyMainTowerHealth;
    /*
     * 
     *     //hoe veel een enemy damage doet
    //hoe veel health (hit points) een enemy heeft
    //hoe snel een enemy beweegt
     * 
     */
    void Start()
    {
        MyEnemyInRage = GetComponent<EnemyRangeScript>();
        MyMainTowerHealth = GetComponent<MainTowerHealth>();
    }

    void DamageGiven()
    {
        
        if (MyEnemyInRage == true)
        {
            MyMainTowerHealth.GetComponent<EnemyAttacking>().damage = damage;
        }
    }

}