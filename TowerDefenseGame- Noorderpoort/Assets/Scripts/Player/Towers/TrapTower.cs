using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        trigger.transform.localScale = new Vector3 (_stats.detectionRange, 0.5f, _stats.detectionRange);
        blastVisual.transform.localScale = new Vector3(_stats.range, 0.5f, _stats.range);
        damage = _stats.damage;
        towerStats = _stats;
    }
    public void UseTrap(Collider collider)
    {
        if(isBeingPlaced) { return; }

        if(onCooldown) { return; }

        if (!collider.gameObject.CompareTag("Enemy")) { return; }
        Collider[] allHit = Physics.OverlapSphere(transform.position, towerStats.range/2);

        for(int i = 0; i < allHit.Length; i++)
        {
            print(allHit[i].gameObject.name);
            if (allHit[i].TryGetComponent(out EnemyHP en))
            {
                en.TakeDamage(damage);
            }
        }
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
