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
    [SerializeField] private RagdollHandler ragdollHandler;

    private NavMeshAgent agent;
    private Transform player;
    private EnemyState state;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float viewAngle = 80f;
    [SerializeField] private float losePlayerTime = 2f;

    [Header("Combat")]
    [SerializeField] private float attackRange = 3f;

    private float timeSinceLostSeenPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
        if (state == EnemyState.Dead || player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case EnemyState.Idle:

                if (distanceToPlayer <= detectionRange && CanSeePlayer())
                {
                    ChangeState(EnemyState.Chase);
                }

                break;

            case EnemyState.Chase:

                ChasePlayer();

                if (distanceToPlayer <= attackRange)
                {
                    ChangeState(EnemyState.Attack);
                }
                else if (distanceToPlayer > detectionRange || !HasClearPathToPlayer())
                {
                    timeSinceLostSeenPlayer += Time.deltaTime;

                    if (timeSinceLostSeenPlayer >= losePlayerTime)
                    {
                        ChangeState(EnemyState.Idle);
                    }
                }
                else
                {
                    timeSinceLostSeenPlayer = 0f;
                }

                break;

            case EnemyState.Attack:

                if (distanceToPlayer > attackRange)
                {
                    ChangeState(EnemyState.Chase);
                }

                break;
        }
    }

    private void ChasePlayer()
    {
        if (agent == null || !agent.enabled)
            return;

        if (agent.isStopped)
            agent.isStopped = false;

        agent.SetDestination(player.position);
    }

    private bool CanSeePlayer()
    {
        return IsFacingPlayer() && HasClearPathToPlayer();
    }

    private bool IsFacingPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        return angle <= viewAngle * 0.5f;
    }

    private bool HasClearPathToPlayer()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 toPlayer = player.position - origin;

        if (Physics.Raycast(origin, toPlayer.normalized, out RaycastHit hit, toPlayer.magnitude))
        {
            return hit.transform == player || hit.transform.IsChildOf(player);
        }

        return false;
    }

    public void Die()
    {
        if (state == EnemyState.Dead)
            return;

        ChangeState(EnemyState.Dead);

        ragdollHandler?.EnableRagdoll();
    }

    private void ChangeState(EnemyState newState)
    {
        if (state == newState)
            return;

        state = newState;

        switch (newState)
        {
            case EnemyState.Idle:

                if (agent != null && agent.enabled)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                }

                break;

            case EnemyState.Chase:

                if (agent != null && agent.enabled)
                {
                    agent.isStopped = false;
                }

                break;

            case EnemyState.Attack:

                if (agent != null && agent.enabled)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    agent.velocity = Vector3.zero;
                }

                break;

            case EnemyState.Dead:

                if (agent != null && agent.enabled)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    agent.enabled = false;
                }

                break;
        }
    }
}