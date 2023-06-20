using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Health")]
    private EnemyStats stats;

    [Header("Money")]
    private int bitsOnDeath;

    [Header("Sound")]
    [SerializeField] private GameObject soundBlock;
    [SerializeField]private AudioClip deathSound;
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        bitsOnDeath = stats.bitsOnDeath;
    }
    public void takeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            die();
        }
    }
    public void die()
    {
        //Money
        Bitscript bits = FindObjectOfType<Bitscript>();
        bits.AddBits(bitsOnDeath);

        GameObject sound = Instantiate(soundBlock, transform.position, transform.rotation);
        sound.GetComponent<AudioSource>().clip = deathSound;
        sound.GetComponent<AudioSource>().Play();
        //Death
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Death if touched by a bullet and less than 1 hp
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (stats.health <= 0)
            {
                die();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Death if touched by a trap
        if (other.gameObject.CompareTag("Trap"))
        {
            takeDamage(300);
            Destroy(other.gameObject);
        }
    }
}
