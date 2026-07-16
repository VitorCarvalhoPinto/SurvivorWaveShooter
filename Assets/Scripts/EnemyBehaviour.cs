using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Dead
}

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public RagdollHandler ragdollHandler;

    public float detectionRange;
    public float viewAngle = 90f;
    public float loseInterestRange = 10f;
    public float losePlayerTime = 15f;
    public float attackRange = 5f;
    private float timeSinceLostSeenPlayer;

    private EnemyState state;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on enemy: " + name);
        }

        state = EnemyState.Idle;
        if (agent != null) agent.isStopped = true;
    }

    private void Update()
    {
        // Se estiver morto, para de processar a IA imediatamente.
        if (state == EnemyState.Dead) return;

        var distanceToPlayer = Vector3.Distance(player.position, transform.position);

        switch (state)
        {
            case EnemyState.Idle:
                if (player != null)
                {
                    if (distanceToPlayer <= detectionRange && CanSeePlayer())
                    {
                        state = EnemyState.Chase;
                        if (agent != null) agent.isStopped = false;
                    }
                }
                break;

            case EnemyState.Chase:
                ChasePlayer();

                if (distanceToPlayer <= attackRange)
                {
                    state = EnemyState.Attack;
                }
                else if (!CanSeePlayer())
                {
                    timeSinceLostSeenPlayer += Time.deltaTime;
                    if (timeSinceLostSeenPlayer >= losePlayerTime)
                    {
                        state = EnemyState.Idle;
                    }
                }
                else
                {
                    timeSinceLostSeenPlayer = 0f;
                }
                break;

            case EnemyState.Attack:
                Attack();

                if (distanceToPlayer > attackRange)
                {
                    state = EnemyState.Chase;
                    if (agent != null) agent.isStopped = false;
                }
                break;
        }
    }

    private bool CanSeePlayer()
    {
        return IsFacingPlayer() && HasClearPathToPlayer();
    }

    private bool IsFacingPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        return angleToPlayer < viewAngle / 2f;
    }

    private bool HasClearPathToPlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        float distance = toPlayer.magnitude;
        Vector3 directionToPlayer = toPlayer.normalized;

        if (Physics.Raycast(origin, directionToPlayer, out RaycastHit hit, distance))
        {
            return hit.transform == player || hit.transform.IsChildOf(player);
        }
        return false;
    }

    private void ChasePlayer()
    {
        if (agent == null) return;
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        if (agent != null) agent.isStopped = true;

        var direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        Debug.Log($"Attacking player!");
    }

    public void Die()
    {
        Debug.Log("ALOOOOOOOOOOOOOOOOOOOOO");
        if (state == EnemyState.Dead) return;

        state = EnemyState.Dead;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
            Debug.Log("NavMeshAgent desligado!"); // Se não aparecer, o código nunca passou por aqui
        }

        if (ragdollHandler != null)
        {
            ragdollHandler.EnableRagdoll();
            Debug.Log("Ragdoll ativado!");
        }
    }
}