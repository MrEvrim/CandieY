using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTPS : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform; // Karakterin transformu
    [SerializeField]
    private float distance = 5.0f; // Kamera mesafesi
    [SerializeField]
    private float sensitivity = 2.0f; // Fare hassasiyeti
    [SerializeField]
    private float verticalLimit = 80.0f; // Y eksenindeki açı limiti
    [SerializeField]
    private Transform waist; // Bel transformu
    [SerializeField]
    private GameObject aimTarget; // Bel transformu

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
        Cursor.lockState = CursorLockMode.Locked; // İmleci kilitle
        Cursor.visible = false; // İmleci gizle
    }

    void LateUpdate()
    {
        HandleCameraRotation();
    }

    void HandleCameraRotation()
    {
        // Fare hareketini oku
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Dikey açıyı sınırlama
        rotationY = Mathf.Clamp(rotationY, -verticalLimit, verticalLimit);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Kamerayı hedefin etrafında döndür
        Vector3 position = targetTransform.position - (rotation * Vector3.forward * distance);
        transform.position = position;
        transform.LookAt(targetTransform);

        // Belin yukarı-aşağı rotasyonunu kontrol et
        WaistMove(rotationY);
    }

    // Belin yukarı-aşağı rotasyonu
    public void WaistMove(float verticalRotation)
    {
        // Belin rotasyonunu yalnızca yukarı ve aşağı olacak şekilde ayarla
        Quaternion waistRotation = Quaternion.Euler(verticalRotation, waist.transform.localEulerAngles.y, waist.transform.localEulerAngles.z);
        waist.transform.localRotation = waistRotation;
    }
    //aimtargeti kamera merkezinde tutma x y z 
    public void AimTargetMove()
    {
        aimTarget.transform.position = transform.position + transform.forward * 5f;
        aimTarget.transform.rotation = transform.rotation; 
    }
}
