using UnityEngine;

public class SearchState : EnvironmentInteractionState
{
    public float approachDistanceThreshold = 2.0f;

    public SearchState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate)
        : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Search State");
    }

    public override void ExitState()
    {
        // TODO: Implementar lógica ao sair do estado de busca.
    }

    public override void UpdateState()
    {
        // TODO: Atualizar lógica de busca a cada frame.
    }

    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
    {
        bool isCloseToTarget = Vector3.Distance(Context.ClosestPointsOnColliderFromShoulder, Context.RootTransform.position) < approachDistanceThreshold;
        bool isClosestPointOnColliderValid = Context.ClosestPointsOnColliderFromShoulder != Vector3.positiveInfinity;

        if (isClosestPointOnColliderValid && isCloseToTarget)
        {
            return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Approach;
        }

        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        StartIkTargetPositionTracking(other);
    }

    public override void OnTriggerStay(Collider other)
    {
        UpdateIkTargetPosition(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        ResetIkTargetPositionTracking(other);
    }
}
