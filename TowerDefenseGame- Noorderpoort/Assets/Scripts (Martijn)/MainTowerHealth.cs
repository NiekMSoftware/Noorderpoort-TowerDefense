using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerHealth : MonoBehaviour
{
    public float MTHealth = 1000;
    public int DamageTaken;

    EnemyAttacking MyEnemyAttacking;
    /*
     * MTHealth = int voor Main tower's hp
     */

    // Start is called before the first frame update
    void Start()
    {
        MyEnemyAttacking = GetComponent<EnemyAttacking>();
    }

    public void Attacked()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
