using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class TowerAttacking : MonoBehaviour
{
    public Material testMat;
    [SerializeField] private float health = 100;
    [SerializeField] private float timeStunned = 5;
    float stunTime = 0;
    bool stunned = false;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float timeUntilBullet;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float projectileSpeed;
    bool enemyInRange = false;
    GameObject bulletEmpty;
    [SerializeField] private bool defaultTower = true;
    void Start()
    {
        gameObject.GetComponent<RangeScriptV2>().range = range;
        bulletEmpty = GameObject.Find("BulletEmpty");
    }

    void Update()
    {
        if (target == null)
        {
            enemyInRange = false;
        }
        else
        {
            enemyInRange = true;
        }
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
                target = gameObject.GetComponent<RangeScriptV2>().enemyList[0].gameObject;
                target.GetComponent<MeshRenderer>().material = testMat;
            }
            catch (ArgumentOutOfRangeException)
            {
                if (gameObject.GetComponent<RangeScriptV2>().enemyList.Count == 0)
                {
                    target = null;
                }
            }
            timeUntilBullet -= Time.deltaTime;
            if (enemyInRange == true)
            {
                if (timeUntilBullet < 0)
                {
                    if (defaultTower == true)
                    {
                        GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z - 90));
                        Projectile.transform.parent = bulletEmpty.transform;
                        Projectile.GetComponent<ProjectileController>().target = target.transform;
                        Projectile.GetComponent<ProjectileController>().speed = projectileSpeed / 10;
                        timeUntilBullet = fireRate / 10;
                    }
                }
            }
        }
        else
        {
            stunTime -= Time.deltaTime;
        }
    }
}