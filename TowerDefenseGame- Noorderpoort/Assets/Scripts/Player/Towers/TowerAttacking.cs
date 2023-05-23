using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class TowerAttacking : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float health = 100;
    [Space]
    [SerializeField] private float timeStunned = 5;
    float stunTime = 0;
    bool stunned = false;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [Space]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float range;
    bool enemyInRange = false;

    [Header("Firepoints")]
    [SerializeField] private Transform firePoint;
    [Space]
    [SerializeField] private bool multipleFirepoints;
    [SerializeField] private Transform[] allFirePoints;
    int currentFirePoint = 0;

    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    float timeUntilBullet;
    GameObject bulletEmpty;

    [Header("Other")]
    [SerializeField] private GameObject target;
    [SerializeField] private bool rotatesTowardsEnemies;
    public bool isBeingPlaced = false;
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
                if (gameObject.GetComponent<RangeScriptV2>().enemyList[0].gameObject != null)
                {
                    target = gameObject.GetComponent<RangeScriptV2>().enemyList[0].gameObject;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                if (gameObject.GetComponent<RangeScriptV2>().enemyList.Count == 0)
                {
                    target = null;
                }
            }

            if (isBeingPlaced == false)
            {
                timeUntilBullet -= Time.deltaTime;
                if (enemyInRange == true)
                {
                    if (timeUntilBullet < 0)
                    {
                        GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z - 90));

                        Projectile.transform.parent = bulletEmpty.transform;
                        Projectile.GetComponent<ProjectileController>().target = target.transform;
                        Projectile.GetComponent<ProjectileController>().speed = projectileSpeed / 10;

                        timeUntilBullet = fireRate / 10;
                        target.GetComponent<EnemyHP>().takeDamage(damage);
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
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(-lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }
}