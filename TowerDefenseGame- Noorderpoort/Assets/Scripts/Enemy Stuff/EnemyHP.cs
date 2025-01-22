using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    [Header("Health")]
    public EnemyScriptable scriptable;
    public float health;

    [Header("Money")]
    private int bitsOnDeath;

    [Header("Sound")]
    [SerializeField] private GameObject soundBlock;
    [SerializeField]private AudioClip deathSound;

    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        //Setting stats
        health = scriptable.health;
        if(agent == null) { agent = gameObject.GetComponent<NavMeshAgent>(); }
        agent.speed = scriptable.speed;
        bitsOnDeath = scriptable.bitsOnDeath;

        //Making hp scale with waves
        health *= FindObjectOfType<WaveSystem>().enemyHealthMultiplier;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //Money
        Bitscript bits = FindObjectOfType<Bitscript>();
        bits.AddBits(bitsOnDeath);

        if (scriptable.playSoundOnDeath)
        {
            //Spawn and play a death sound
            GameObject sound = Instantiate(soundBlock, transform.position, transform.rotation);
            sound.GetComponent<AudioSource>().clip = deathSound;
            sound.GetComponent<AudioSource>().Play();
        }
        //Death
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Death if touched by a bullet and less than 1 hp, no matter the bullets damage
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
