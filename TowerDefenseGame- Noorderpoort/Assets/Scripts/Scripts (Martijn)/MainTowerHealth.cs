using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerHealth : MonoBehaviour
{ 
     public float MTHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAttacking>() != null)
        {
            collision.gameObject.GetComponent<EnemyAttacking>().MTInRange = true;
            collision.gameObject.GetComponent<EnemyAttacking>().mainTowerHealth = FindObjectOfType<Healthscript>();/*gameObject.GetComponent<Healthscript>();*/
        }
    }

}
