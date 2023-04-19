using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerHealth : MonoBehaviour
{
    public int MTHealth = 1000;
    public int DamageTaken;
    EnemyAttacking MyEnemyAttacking;
    /*
     * MTHealth = int voor Main tower's hp
     * DamageTaken = hoeveel dmg er de tower krijgt
     */

    // Start is called before the first frame update
    void Start()
    {
        MyEnemyAttacking = GetComponent<EnemyAttacking>();
    }
    void Damage()
    {
        if (MyEnemyAttacking == true)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
