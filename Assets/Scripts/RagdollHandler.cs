using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider mainCollider; // Adicionado SerializeField aqui!

    private Rigidbody[] ragdollRigidbodies;
    public bool isRagdoll;

    private void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        isRagdoll = true;
        animator.enabled = false;

        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void DisableRagdoll()
    {
        isRagdoll = false;
        animator.enabled = true;

        if (mainCollider != null)
        {
            mainCollider.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}