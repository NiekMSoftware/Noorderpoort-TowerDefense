using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Bullet Time To Live")]
    [SerializeField] private float bulletTTL = 5;
    [Header("Other")]
    public GameObject target;
    private EnemyHP targetHP;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private bool justHereToCount;
    [Space]
    public float speed = 0.1f;
    private Vector3 targetDirection;

    public float damage;
    private Rigidbody rb;
    void Start()
    {
        //Where it wants to go
        targetDirection = target.transform.position - transform.position;
        targetHP = target.GetComponent<EnemyHP>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //For any non projectile objects this script can be used as a death timer
        if (justHereToCount == false)
        {
            Vector3 velo = rb.velocity;
            if (velo != Vector3.zero) { transform.rotation = Quaternion.LookRotation(velo); }
            rb.AddForce(targetDirection * speed, ForceMode.Impulse);
        }

        //Bullet dies after existing too long
        bulletTTL -= Time.deltaTime;
        if (bulletTTL < 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (justHereToCount == false)
        {
            //Bullet dies after hitting the enemy, damage is handled by tower attacking
            if (collision.collider.CompareTag("Enemy"))
            {
                Instantiate(hitParticle, transform.position, transform.rotation);
                Die();
            } 
        }
    }

    public void Die()
    {
        if(justHereToCount == false && target != null)
        {
            //If the bullet misses it deals damage anyway
            targetHP.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
