using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    [Header("Range")]
    public float range;
    [Header("Enemies")]
    public List<Collider> enemyList;
    public TowerAttacking towerAttack;
    [SerializeField] private GameObject rangeCircle;
    public List<GameObject> activateRanges;
    private void Start()
    {
        towerAttack = gameObject.GetComponent<TowerAttacking>();
        //ShowRange();
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
                    if (col.gameObject.GetComponent<EnemyNavMesh>() == true && enemyList.Contains(col) == false && col.gameObject.CompareTag("Enemy"))
                    {
                        //Adds them to a list
                        enemyList.Add(col);
                    }
                }
                foreach (Collider col in enemyList.ToList())
                {
                    //Removes the enemies if they get out of range
                    if (col)
                    {
                        if (hitColliders.Contains(col) == false)
                        {
                            if (towerAttack.target == col.gameObject) { towerAttack.target = null; }
                            enemyList.Remove(col);
                        }
                    }
                    else
                    {
                        enemyList.Remove(col);
                    }
                }
            }
        }
    }

    public void ShowRange(bool show = true,bool follow = false)
    {
        if (show)
        {
            GameObject cir = Instantiate(rangeCircle);
            cir.transform.position = transform.position;
            cir.transform.localScale = new Vector3(range * 2, rangeCircle.transform.localScale.y, range * 2);
            if (follow) { cir.transform.parent = transform;  }
            activateRanges.Add(cir);
        }
        else if (!show)
        {
            foreach(GameObject ob in activateRanges)
            {
                Destroy(ob);
            }
            activateRanges.Clear();
        }
    }
}
