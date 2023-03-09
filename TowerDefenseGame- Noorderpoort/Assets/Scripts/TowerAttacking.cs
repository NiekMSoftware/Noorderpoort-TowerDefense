using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttacking : MonoBehaviour
{
    public float health = 100;
    public float timeStunned = 5;
    float stunTime = 0;
    bool stunned = false;
    public GameObject target;
    public GameObject projectile;
    public GameObject rangeCircle;
    public Transform firePoint;
    public float fireRate;
    public float timeUntilBullet;
    public float damage;
    public float range;
    public float projectileSpeed;
    public bool enemyInRange = false;
    [SerializeField] private bool defaultTower = true;
    [SerializeField] private bool IforgotTheName;
    void Start()
    {
        rangeCircle.transform.localScale = new Vector3(range , rangeCircle.transform.localScale.y, range);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            health--;
        }
        if (health <= 0)
        {
            stunned = true;
            stunTime = timeStunned;
            health = 100;
        }
        if (stunTime <= 0 && stunned == true)
        {
            health = 100;
            stunned = false;
        }
        if (stunned == false)
        {

            try
            {
                target = rangeCircle.GetComponent<RangeScript>().inRangeList[0];
            } catch (ArgumentOutOfRangeException)
            {
                Debug.Log("No Argument!");
            }
            timeUntilBullet -= Time.deltaTime;
            if (enemyInRange == true)
            {
                if (timeUntilBullet < 0)
                {
                    if (defaultTower == true)
                    {
                        GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z - 90));
                        Projectile.GetComponent<ProjectileController>().target = target.transform;
                        Projectile.GetComponent<ProjectileController>().speed = projectileSpeed / 10;
                        timeUntilBullet = fireRate / 10;
                    }
                    if (IforgotTheName == true)
                    {
                        GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, 100, firePoint.eulerAngles.z - 90));
                        Projectile.GetComponent<ProjectileController>().target = target.transform;
                        Projectile.GetComponent<ProjectileController>().speed = projectileSpeed / 10;
                        timeUntilBullet = fireRate / 10;
                    }
                }
            }
            enemyInRange = rangeCircle.GetComponent<RangeScript>().enemyInRange;
        }
        else
        {
            stunTime -= Time.deltaTime;
        }
    }
}
