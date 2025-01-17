using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEnemyLooking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<EnemyLooking>())
        {
            other.GetComponent<EnemyLooking>().lookingAtBase = true;
            other.GetComponent<EnemyLooking>().Base = gameObject.transform;
            other.GetComponent<EnemyNavMesh>().lookingAtMovement = false;
        }
    }
}
