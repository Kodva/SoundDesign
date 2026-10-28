
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerInput p;
    public Transform cameraTransform;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Vector2 movementInput;
    private bool jumpRequested;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementInput = p.actions["Move"].ReadValue<Vector2>();

        if (p.actions["Jump"].WasPressedThisFrame())
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        CheckGround();

        if (jumpRequested && isGrounded)
        {
            // FUNCION DE SONIDO DE SALTO AQUI
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        jumpRequested = false;


        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            forward * movementInput.y +
            right * movementInput.x;

        movement = Vector3.ClampMagnitude(movement, 1f);


        Vector3 velocity = rb.linearVelocity;
        velocity.x = movement.x * moveSpeed;
        velocity.z = movement.z * moveSpeed;
        rb.linearVelocity = velocity;

        if (movement.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        Debug.DrawRay(
            transform.position,
            Vector3.down * groundCheckDistance,
            isGrounded ? Color.green : Color.red
        );
    }
}
