using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 targetOffset;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float zoomSpeed = 10f;
    [SerializeField]
    private float minZoom = 5f;
    [SerializeField]
    private float maxZoom = 20f;

    private float currentZoom = 10f;

    void Start()
    {
        currentZoom = targetOffset.magnitude;  
    }

    void Update()
    {
        HandleZoom();
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 desiredPosition = target.position - transform.forward * currentZoom + targetOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, movementSpeed * Time.deltaTime);
    }

    void HandleZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        currentZoom -= scrollData * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);  
    }
}
