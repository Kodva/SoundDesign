
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 5f;
    public float height = 1.5f;
    public float mouseSensitivity = 0.15f;

    [Header("Vertical Rotation")]
    public float minPitch = -30f;
    public float maxPitch = 70f;

    private float yaw;
    private float pitch = 15f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null || Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 pivot = target.position + Vector3.up * height;

        transform.position = pivot - rotation * Vector3.forward * distance;
        transform.rotation = rotation;
    }
}
