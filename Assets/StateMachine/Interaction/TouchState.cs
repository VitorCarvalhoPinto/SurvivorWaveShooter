using UnityEngine;

public class TouchState : EnvironmentInteractionState
{
    public TouchState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate)
        : base(context, estate)
    {
    }

    public override void EnterState()
    {
        // TODO: Implementar lógica ao entrar no estado de toque.
    }

    public override void ExitState()
    {
        // TODO: Implementar lógica ao sair do estado de toque.
    }

    public override void UpdateState()
    {
        // TODO: Atualizar lógica de toque a cada frame.
    }

    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
    {
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // TODO: Tratar trigger ao iniciar o toque.
    }

    public override void OnTriggerStay(Collider other)
    {
        // TODO: Tratar trigger enquanto estiver tocando.
    }

    public override void OnTriggerExit(Collider other)
    {
        // TODO: Tratar trigger ao sair do estado de toque.
    }
}
