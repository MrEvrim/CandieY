using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTPS : MonoBehaviour
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
        UpdateMovement();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void UpdateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift);
        movementSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * vertical + right * horizontal).normalized;
        isMoving = movement != Vector3.zero;

        Vector3 moveDirection = movement * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDirection);
    }

    void ApplyMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isMoving && isRunning);
    }

    
}
