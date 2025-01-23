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
    [SerializeField] private GameObject rangeCircle;
    public List<GameObject> activateRanges;
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
            //Spawn a range circle
            GameObject cir = Instantiate(rangeCircle);
            cir.transform.position = transform.position;
            cir.transform.localScale = new Vector3(range * 2, rangeCircle.transform.localScale.y, range * 2);

            //Makes them follow the parent if they are told to, but dont is they arent. This is so they dont rotate with towers while placed
            if (follow) { cir.transform.parent = transform;  }

            //Adds the circle to the list, since this function sometimes gets called multiple times
            activateRanges.Add(cir);
        }
        else if (!show)
        {
            //Removes all active range cirlces
            foreach(GameObject ob in activateRanges)
            {
                Destroy(ob);
            }
            activateRanges.Clear();
        }
    }
}
