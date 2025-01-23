using UnityEngine;

public class SwitchEnemyLooking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if enemy gets in range, make it look at the main base
        if (other.CompareTag("Enemy") && other.GetComponent<EnemyLooking>())
        {
            other.GetComponent<EnemyLooking>().lookingAtBase = true;
            other.GetComponent<EnemyLooking>().Base = gameObject.transform;
            other.GetComponent<EnemyNavMesh>().lookingAtMovement = false;
        }
    }
}
