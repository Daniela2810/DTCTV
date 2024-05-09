using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveCamera : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float movementSpeed = 5.0f;

    [Header("Sensibilidad del mouse")]
    public float mouseSensitivity = 2.0f;

    private float verticalRotation;
    private float horizontalRotation;
    private bool isLocked = false;
    private Camera mainCamera;

    private Rigidbody rb;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        horizontalRotation = transform.eulerAngles.y;
        verticalRotation = mainCamera.transform.localEulerAngles.x;

        if (verticalRotation > 180)
            verticalRotation -= 360;
    }

    void Update()
    {
        if (!isLocked)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 movement = cameraForward * verticalMovement + mainCamera.transform.right * horizontalMovement;
        rb.velocity = movement.normalized * movementSpeed;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalRotation += mouseX;
        verticalRotation = Mathf.Clamp(verticalRotation - mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    public void LockCameraCinematic()
    {
        isLocked = true;
    }

    public void LockCamera()
    {
        isLocked = true;
    }

    public void UnlockCamera()
    {
        isLocked = false;
    }
}