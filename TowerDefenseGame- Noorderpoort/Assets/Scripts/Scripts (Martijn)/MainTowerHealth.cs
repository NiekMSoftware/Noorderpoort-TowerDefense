using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerHealth : MonoBehaviour
{ 
    public float MTHealth;
    private Healthscript healthscript;


    // Start is called before the first frame update
    void Start()
    {
        healthscript = FindObjectOfType<Healthscript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHP>() != null)
        {
            EnemyHP hp = collision.gameObject.GetComponent<EnemyHP>();
            healthscript.HealthIndex -= hp.scriptable.mainTowerDamage;
            hp.TakeDamage(9999999999999999);
        }
    }

}
