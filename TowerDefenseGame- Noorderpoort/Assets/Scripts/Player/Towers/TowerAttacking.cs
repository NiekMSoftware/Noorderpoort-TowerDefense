using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class TowerAttacking : GeneralTowerScript
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

    [Header("Sound")]
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip attackSound;

    [Header("Other")]
    [SerializeField] private GameObject target;
    [SerializeField] private RangeScript rangeScript;
    [SerializeField] private bool rotatesTowardsEnemies;
    void Start()
    {
        if (audiosource != null)
        {
            audiosource.clip = attackSound;
        }
        bulletEmpty = GameObject.Find("BulletEmpty");
        if (multipleFirepoints == true)
        {
            firePoint = allFirePoints[currentFirePoint];
        }
        rangeScript = gameObject.GetComponent<RangeScript>();
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
            if (rangeScript.enemyList.Count != 0)
            {
                if (rangeScript.enemyList[0] != null)
                {
                    target = rangeScript.enemyList[0].gameObject;
                }
                else if (rangeScript.enemyList.Count == 0)
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
                        target.GetComponent<EnemyHP>().TakeDamage(damage);
                        if (multipleFirepoints)
                        {
                            firePoint = allFirePoints[currentFirePoint];
                            currentFirePoint++;
                            if (currentFirePoint == allFirePoints.Length)
                            {
                                currentFirePoint = 0;
                            }
                        }
                        audiosource.Play();
                    }
                }
            }
        }
        else
        {
            stunTime -= Time.deltaTime;
        }
        if (gameObject.GetComponent<RangeScript>().enemyList.Count != 0)
        {
            if (rotatesTowardsEnemies)
            {
                if (target == null) return;
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(-lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }

    public override void SetStats(TowerScriptable stats)
    {
        if(rangeScript == null) { rangeScript = gameObject.GetComponent<RangeScript>(); }
        damage = stats.damage;
        fireRate = stats.firerate;
        projectileSpeed = stats.projectileSpeed;
        rangeScript.range = stats.range;
        rangeScript.ShowRange();
        sellValue = stats.sellValue;
        towerStats = stats;
    }
}