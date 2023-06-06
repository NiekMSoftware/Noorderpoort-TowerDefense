using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    [Header("Range")]
    public float range;
    [Header("Enemies")]
    public List<Collider> enemyList;
    public TowerAttacking towerAttack;
    private void Start()
    {
        towerAttack = gameObject.GetComponent<TowerAttacking>();
    }
    void Update()
    {
        if (towerAttack.isBeingPlaced == false)
        {
            //Looks at everything around it
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, range);
            if (hitColliders.Length > 0)
            {
                foreach (Collider col in hitColliders.ToList())
                {
                    //Makes sure that the things are enemies
                    if (col.gameObject.GetComponent<EnemyNavMesh>() == true && enemyList.Contains(col) == false)
                    {
                        //Adds them to a list
                        enemyList.Add(col);
                    }
                }
                foreach (Collider col in enemyList.ToList())
                {
                    //Removes the enemies if they get out of range
                    if (hitColliders.Contains(col) == false)
                    {
                        enemyList.Remove(col);
                    }
                }
            }
        }
    }
}
