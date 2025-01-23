using UnityEngine;

public class TowerAttacking : GeneralTowerScript
{
    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    private bool enemyInRange = false;

    [Header("Firepoints")]
    [SerializeField] private Transform firePoint;
    [Space]
    [SerializeField] private bool multipleFirepoints;
    [SerializeField] private Transform[] allFirePoints;
    private int currentFirePoint = 0;

    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    private float timeUntilNextBullet;
    private GameObject bulletEmpty;

    [Header("Sound")]
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip attackSound;

    [Header("Other")]
    public GameObject target;
    [SerializeField] private RangeScript rangeScript;
    [SerializeField] private bool rotatesTowardsEnemies;
    void Start()
    {
        if (audiosource != null)
        {
            audiosource.clip = attackSound;
        }
        bulletEmpty = GameObject.Find("BulletEmpty");

        //Sets the current firepoint and gets the range
        if (multipleFirepoints == true)
        {
            firePoint = allFirePoints[currentFirePoint];
        }
        rangeScript = gameObject.GetComponent<RangeScript>();
    }

    void Update()
    {
        //Checks if an enemy exists
        if (target == null)
        {
            enemyInRange = false;
        }
        else
        {
            enemyInRange = true;
        }

        //Gets the first enemy that entered its range
        if (rangeScript.enemyList.Count != 0 && target == null)
        {
            if (rangeScript.enemyList[0] != null)
            {
                target = rangeScript.enemyList[0].gameObject;
            }
        }

        if (isBeingPlaced == false)
        {
            timeUntilNextBullet -= Time.deltaTime;
            if (enemyInRange == true)
            {
                if (timeUntilNextBullet < 0)
                {
                    //Spawn bullet
                    GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z - 90));

                    Projectile.transform.parent = bulletEmpty.transform;
                    ProjectileController control = Projectile.GetComponent<ProjectileController>();
                    control.target = target;
                    control.speed = projectileSpeed / 10;
                    control.damage = towerStats.damage;

                    timeUntilNextBullet = fireRate / 10;

                    //Choose next firepoint
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
        
        if (rangeScript.enemyList.Count != 0)
        {
            if (rotatesTowardsEnemies)
            {
                //Look towards your target
                if (target == null) return;
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(-lookPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }

    public override void SetStats(TowerScriptable stats)
    {
        //Self explanatory
        if(rangeScript == null) { rangeScript = gameObject.GetComponent<RangeScript>(); }
        damage = stats.damage;
        fireRate = stats.firerate;
        projectileSpeed = stats.projectileSpeed;
        rangeScript.range = stats.range;
        towerStats = stats;
    }
}