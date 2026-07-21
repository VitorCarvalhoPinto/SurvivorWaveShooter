using UnityEngine;

public class ResetState : EnvironmentInteractionState
{
    float elapsedTime = 0f;
    float resetDuration = 1.8f;
    private Vector3 _lastPosition;
    private bool _hasLastPosition;
    private bool _isMoving;

    public ResetState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate)
        : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Reset state");
        elapsedTime = 0f;
        _isMoving = false;
        _hasLastPosition = false;
        Context.ClosestPointsOnColliderFromShoulder = Vector3.positiveInfinity;
        Context.CurrentIntersectinCollider = null;
    }
    public override void ExitState() { }
    public override void UpdateState()
    {
        elapsedTime += Time.deltaTime;

        if (Context.RootTransform != null)
        {
            if (_hasLastPosition)
            {
                Vector3 delta = Context.RootTransform.position - _lastPosition;
                _isMoving = delta.sqrMagnitude > 0.0001f;
            }

            _lastPosition = Context.RootTransform.position;
            _hasLastPosition = true;
        }
    }
    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
    {
        bool hasRigidBodyVelocity = Context.Rigidbody != null && Context.Rigidbody.linearVelocity.sqrMagnitude > 0.0001f;
        bool isMoving = _isMoving || hasRigidBodyVelocity;

        Debug.Log($"{isMoving} | linearVelocity={Context.Rigidbody?.linearVelocity} | lastPositionDelta={_isMoving}");

        if (elapsedTime >= resetDuration && isMoving)
        {
            return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Search;
        }
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
