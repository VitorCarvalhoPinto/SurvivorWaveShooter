using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cam;
    public Vector2 lookInput;
    private float xRotation = 0f;
    private Vector2 zTilt;

    // Camera tilt
    public float tiltAmount = 1.5f;
    public float tiltSpeed = 8f;
    private float currentTilt = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main.transform;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void HandleMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        float targetTilt = -zTilt.x * tiltAmount;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSpeed * Time.deltaTime);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, currentTilt);
        transform.Rotate(Vector3.up * mouseX);
    }


    void OnMovement(InputValue value)
    {
        zTilt = value.Get<Vector2>();
    }
}