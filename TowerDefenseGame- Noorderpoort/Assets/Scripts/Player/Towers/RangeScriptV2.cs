using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeScriptV2 : MonoBehaviour
{
    public List<Collider> enemyList;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, range);
        if (hitColliders.Length > 0)
        {
            foreach (Collider col in hitColliders.ToList())
            {
                if (col.gameObject.GetComponent<EnemyNavMesh>() == true && enemyList.Contains(col) == false)
                {
                    enemyList.Add(col);
                }
            }
            foreach (Collider col in enemyList.ToList())
            {
                if (hitColliders.Contains(col) == false)
                {
                    enemyList.Remove(col);
                }
            }
        }
    }
}
