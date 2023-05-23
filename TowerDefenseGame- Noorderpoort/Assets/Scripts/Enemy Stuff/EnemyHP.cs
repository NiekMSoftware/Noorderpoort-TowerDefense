using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Health")]
    public float hp = 100;
    float ttDestroy = 2;

    [Header("Money")]
    [SerializeField] private int bitsOnDeath;
    void Start()
    {
        //Health multiplier
        hp = hp * FindObjectOfType<WaveSystem>().enemyHealthMultiplier;
    }

    void Update()
    {
        //Fucking dies after an amount of time after your hp is 0
        if (hp <= 0)
        {
            ttDestroy -= Time.deltaTime;
            if (ttDestroy <= 0)
            {
                die();
            }
        }
    }
    public void takeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
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
            if (hp <= 0)
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
