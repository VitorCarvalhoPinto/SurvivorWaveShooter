using UnityEngine;

public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEnvironmentInteractionState>
{
    protected EnvironmentInteractionContext Context;

    public EnvironmentInteractionState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState
    stateKey) : base(stateKey)
    {
        Context = context;
    }

    private Vector3 GetClosestPointOnCollider(Collider intersectingCollider, Vector3 positionToCheck)
    {
        if (intersectingCollider == null)
        {
            return positionToCheck;
        }

        if (intersectingCollider is BoxCollider ||
            intersectingCollider is SphereCollider ||
            intersectingCollider is CapsuleCollider ||
            (intersectingCollider is MeshCollider meshCollider && meshCollider.convex))
        {
            return intersectingCollider.ClosestPoint(positionToCheck);
        }

        return intersectingCollider.ClosestPointOnBounds(positionToCheck);
    }

    protected void StartIkTargetPositionTracking(Collider intersectingCollider)
    {
        if (intersectingCollider.gameObject.layer == LayerMask.NameToLayer("Interactable") && Context.CurrentIntersectinCollider == null)
        {
            Context.CurrentIntersectinCollider = intersectingCollider;
            Vector3 closestPointFromRoot = GetClosestPointOnCollider(intersectingCollider, Context.RootTransform.position);
            Context.SetCurrentSide(closestPointFromRoot);

            SetIkTargetPosition();
        }
    }

    protected void UpdateIkTargetPosition(Collider intersectingCollider)
    {
        if (Context.CurrentIntersectinCollider == intersectingCollider)
        {
            SetIkTargetPosition();
        }
    }

    protected void ResetIkTargetPositionTracking(Collider intersectingCollider)
    {
        if (Context.CurrentIntersectinCollider == intersectingCollider)
        {
            Context.CurrentIntersectinCollider = null;
            Context.ClosestPointsOnColliderFromShoulder = Vector3.positiveInfinity;
        }
    }

    private void SetIkTargetPosition()
    {
        Context.ClosestPointsOnColliderFromShoulder = GetClosestPointOnCollider(Context.CurrentIntersectinCollider,
        new Vector3(Context.CurrentShoulderTransform.position.x, Context.CharacterShoulderHeight, Context.CurrentShoulderTransform.position.z));
    
        Vector3 rayDirection = Context.CurrentShoulderTransform.position - Context.ClosestPointsOnColliderFromShoulder;
        Vector3 normalizedRayDirection = rayDirection.normalized;
        float offsetDistance = .05f;
        Vector3 offset = normalizedRayDirection * offsetDistance;

        Vector3 offsetPosition = Context.ClosestPointsOnColliderFromShoulder + offset;
        Context.CurrentIkTargetTransform.position = offsetPosition;
    }
}
