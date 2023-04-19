using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp = 100;
    float ttDestroy = 2;
    [SerializeField] private int bitsOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        hp = hp * FindObjectOfType<WaveSystem>().enemyHealthMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
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
        Bitscript bits = FindObjectOfType<Bitscript>();
        bits.AddBits(bitsOnDeath);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
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
        if (other.gameObject.CompareTag("Trap"))
        {
            takeDamage(10);
            Destroy(other.gameObject);
        }
    }
}
