using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 5f;  
    [SerializeField]
    private int damage = 20;     

    void Start()
    {
        Destroy(gameObject, lifeTime);  
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);  
            }
        }

        Destroy(gameObject);  
    }
}
