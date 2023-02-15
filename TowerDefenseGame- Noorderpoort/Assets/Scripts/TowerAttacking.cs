using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttacking : MonoBehaviour
{
    public GameObject target;
    public GameObject projectile;
    public GameObject rangeCircle;
    public Transform firePoint;
    public float fireRate;
    float timeUntilBullet;
    public float damage;
    public float range;
    public float projectileSpeed;
    public bool enemyInRange = false;
    void Start()
    {
        rangeCircle.transform.localScale = new Vector3(range , rangeCircle.transform.localScale.y, range);
    }

    void Update()
    {
        timeUntilBullet -= Time.deltaTime;
        if (timeUntilBullet < 0 && enemyInRange == true)
        {
            GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z - 90));
            Projectile.GetComponent<ProjectileController>().target = target.transform;
            Projectile.GetComponent<ProjectileController>().speed = projectileSpeed/10;
            timeUntilBullet = fireRate / 10;
        }
        enemyInRange = rangeCircle.GetComponent<RangeScript>().enemyInRange;
    }
}
