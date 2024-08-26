using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 4f; 
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rb;

    private float movementSpeed;
    private bool isRunning;
    private bool isMoving;

    void Start()
    {
        movementSpeed = walkSpeed;
    }

    void Update()
    { 
        HandleRotationInput();
        UpdateAnimations();
    }
    void FixedUpdate()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift);
        movementSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        isMoving = movement != Vector3.zero;

        Vector3 moveDirection = movement * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDirection);  // Rigidbody ile hareket ettir
    }

    //karakterin titremesi burdan çözülebilir fakar blur gibi durdugu için kalmasını tercih ettim. c:
    void HandleRotationInput()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position); 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  

        float hitDist;
        if (playerPlane.Raycast(ray, out hitDist)) 
        {
            Vector3 targetPoint = ray.GetPoint(hitDist); 
            Vector3 direction = targetPoint - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isMoving && isRunning);
    }
}
