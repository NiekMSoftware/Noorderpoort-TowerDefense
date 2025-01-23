using UnityEngine;

public class MainTowerHealth : MonoBehaviour
{ 
    private Healthscript healthscript;

    void Start()
    {
        healthscript = FindObjectOfType<Healthscript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if you get hit by an enemy, instant kill enemy and have it deal its damage
        if (collision.gameObject.GetComponent<EnemyHP>() != null)
        {
            EnemyHP hp = collision.gameObject.GetComponent<EnemyHP>();
            healthscript.healthAmount -= hp.scriptable.mainTowerDamage;
            hp.TakeDamage(9999999999999999);
        }
    }

}
