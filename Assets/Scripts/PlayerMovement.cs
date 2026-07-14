using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    
    // player movement variables
    public float moveSpeed = 5f;
    public float normalSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;

    // dive variables
    public float diveForwardForce = 8f;
    public float diveDownForce = 3f;
    private bool isDiving = false;

    // ground check variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Crouch variables
    private bool isCrouching = false;
    private CapsuleCollider collider;
    private Vector3 colliderCenter;

    // Input system variables
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Vector2 movementInput;
    private bool isSprinting;
    private Camera mainCamera;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        mainCamera = Camera.main;
        collider = GetComponent<CapsuleCollider>();
        colliderCenter = collider.center; // Armazena o centro original do collider
    }

    void FixedUpdate()
    {
        MovePlayer();
        CheckGround();
    }


    void MovePlayer()
    {
        // Durante o dive, deixa a física controlar o movimento (não sobrescreve a velocidade)
        if (isDiving)
            return;

        Vector3 direction = transform.right * movementInput.x + transform.forward * movementInput.y;
        direction.Normalize();
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);
    }

    void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
        if(!isCrouching)
            moveSpeed = value.isPressed ? sprintSpeed : normalSpeed;
    }

    void OnJump()
    {
        if (isGrounded && !isCrouching)
            rb.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
    }

    void OnCrouch()
    {
        if (!isCrouching)
        {
            Debug.Log("Crouch");
            // diminuir moveSpeed, diminuir collider pela metade e reposicionar câmera
            moveSpeed = crouchSpeed;
            collider.height /= 2;
            collider.center = new Vector3(collider.center.x, collider.center.y - collider.height / 2, collider.center.z);
            isCrouching = true;
            Dive();
        }
        else
        {
            Debug.Log("Stand");
            // restaurar moveSpeed, restaurar collider e reposicionar câmera
            moveSpeed = normalSpeed;
            collider.height *= 2;
            collider.center = colliderCenter;
            isCrouching = false;
        }
    }

    void Dive()
    {
        if (!isGrounded && isSprinting && !isDiving)
        {
            Debug.Log("Dive");
            isDiving = true;
            // impulso para frente (direção que o player olha) e para baixo
            Vector3 diveDirection = transform.forward * diveForwardForce + Vector3.down * diveDownForce;
            rb.AddForce(diveDirection, ForceMode.Impulse);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Encerra o dive assim que tocar o chão, devolvendo o controle ao MovePlayer
        if (isGrounded && isDiving)
            isDiving = false;
    }
}
