using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class TowerAttacking : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float timeStunned = 5;
    float stunTime = 0;
    bool stunned = false;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform[] allFirePoints;
    [SerializeField] private bool multipleFirepoints;
    [SerializeField] private bool rotatesTowardsEnemies;
    public int currentFirePoint = 0;
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
        firePoint = allFirePoints[currentFirePoint];
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
                        if (multipleFirepoints)
                        {
                            firePoint = allFirePoints[currentFirePoint];
                            currentFirePoint++;
                            if (currentFirePoint == allFirePoints.Length)
                            {
                                currentFirePoint = 0;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            stunTime -= Time.deltaTime;
        }
        if (gameObject.GetComponent<RangeScriptV2>().enemyList.Count != 0)
        {
            if (rotatesTowardsEnemies)
            {
                Vector3 targetPostition = new Vector3(0,
                                       target.transform.position.y,
                                       0);
                transform.LookAt(targetPostition);
            }
        }
    }
}