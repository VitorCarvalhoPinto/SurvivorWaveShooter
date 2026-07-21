using System;
using UnityEngine;

public class ApproachState : EnvironmentInteractionState
{
    float elapsedTime = 0f;
    float lerpDuration = 5f;
    float approachWeight = .5f;

    public ApproachState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate)
        : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Approach state");
        elapsedTime = 0f;
    }

    public override void ExitState()
    {
        // TODO: Implementar lógica ao sair do estado de aproximação.
    }

    public override void UpdateState()
    {
        elapsedTime += Time.deltaTime;

        Context.CurrentIkConstraint.weight = Mathf.Lerp(Context.CurrentIkConstraint.weight, approachWeight, elapsedTime / lerpDuration);
    }

    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
    {
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
