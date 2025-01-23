using System.Collections;
using UnityEngine;

public class TrapTower : GeneralTowerScript
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject blastVisual;
    private float damage;
    private int usesUsed = 0;
    private bool onCooldown = false;

    public override void SetStats(TowerScriptable _stats)
    {
        //Sets the scale of the trigger and blast radius objects
        trigger.transform.localScale = new Vector3 (_stats.detectionRange, 0.5f, _stats.detectionRange);
        blastVisual.transform.localScale = new Vector3(_stats.range, 0.5f, _stats.range);

        damage = _stats.damage;
        towerStats = _stats;
    }
    public void UseTrap(Collider collider)
    {
        //Trap cant be used if: Being placed, On cooldown, Not touching an enemy
        if(isBeingPlaced) { return; }

        if(onCooldown) { return; }

        if (!collider.gameObject.CompareTag("Enemy")) { return; }

        //Get all enemies in splash area
        Collider[] allHit = Physics.OverlapSphere(transform.position, towerStats.range/2);

        for(int i = 0; i < allHit.Length; i++)
        {
            if (allHit[i].TryGetComponent(out EnemyHP en))
            {
                //Make them take damage
                en.TakeDamage(damage);
            }
        }

        //Destroy objects after too many uses
        usesUsed++;
        if(usesUsed >= towerStats.uses)
        {
            Destroy(gameObject);
            return;
        }

        
        onCooldown = true;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(towerStats.cooldown);
        onCooldown = false;
    }
}
