using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 20f;    
    [SerializeField]
    private float attackRange = 10f;       
    [SerializeField]
    private float moveSpeed = 3f;          
    [SerializeField]
    private float fireRate = 1f;           
    [SerializeField]
    private GameObject bulletPrefab;       
    [SerializeField]
    private Transform firePoint;           
    [SerializeField]
    private Animator animator;             
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioSource audioSource;

    private Transform player;              
    private bool playerInSight = false;    
    private float nextFireTime = 0f;       

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void Update()
    {
        DetectPlayer();
        if (playerInSight)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                Shoot();
            }
        }
        else
        {
            Idle();
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        playerInSight = false;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerInSight = true;
                break;
            }
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isShooting", false);
        animator.SetBool("isIdle", false);

        Vector3 direction = (player.position - transform.position).normalized;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); 
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShooting", true);
        animator.SetBool("isIdle", false);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // sapma 
            Vector3 direction = (player.position - firePoint.position).normalized;

            float randomSpreadAngle = 5f;  
            Quaternion spread = Quaternion.Euler(
                Random.Range(-randomSpreadAngle, randomSpreadAngle),
                Random.Range(-randomSpreadAngle, randomSpreadAngle),
                0
            );

            direction = spread * direction;
            if (audioSource != null && fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }

            bullet.GetComponent<Rigidbody>().AddForce(direction * 20f, ForceMode.Impulse);
        }
    }

    void Idle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShooting", false);
        animator.SetBool("isIdle", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
