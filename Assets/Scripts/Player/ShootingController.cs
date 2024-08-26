using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; 
    [SerializeField]
    private Transform firePoint;     
    [SerializeField]
    private float bulletSpeed = 1f;  
    [SerializeField]
    private float fireRate = 0.5f;   
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fireSound;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }
    }

    void Shoot()
    {
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
            animator.SetTrigger("Shoot"); 
        }
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
        Plane plane = new Plane(Vector3.up, firePoint.position); 

        float distance;
        Vector3 targetPoint;

        if (plane.Raycast(ray, out distance))  
        {
            targetPoint = ray.GetPoint(distance); 
        }
        else
        {
            targetPoint = ray.GetPoint(1000);  
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);  
    }

}