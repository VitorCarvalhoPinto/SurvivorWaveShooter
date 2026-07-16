using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody[] ragdollRigidbodies;

    private PlayerInput input;

    public bool isRagdoll;

    private void Awake()
    {
        input = new PlayerInput();

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        DisableRagdoll();
    }

    // private void OnEnable()
    // {
    //     input.Enable();
    //     input.Player.Ragdoll.performed += OnRagdoll;
    // }

    // private void OnDisable()
    // {
    //     input.Player.Ragdoll.performed -= OnRagdoll;
    //     input.Disable();
    // }

    // private void OnRagdoll(InputAction.CallbackContext context)
    // {
    //     if (isRagdoll)
    //         DisableRagdoll();
    //     else
    //         EnableRagdoll();
    // }

    public void EnableRagdoll()
    {
        isRagdoll = true;
        animator.enabled = false;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void DisableRagdoll()
    {
        isRagdoll = false;
        animator.enabled = true;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}