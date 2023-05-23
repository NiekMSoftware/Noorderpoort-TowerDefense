using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Health")]
    private EnemyStats stats;

    [Header("Money")]
    private int bitsOnDeath;
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        bitsOnDeath = stats.bitsOnDeath;
    }

    void Update()
    {

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
            takeDamage(10);
            Destroy(other.gameObject);
        }
    }
}
