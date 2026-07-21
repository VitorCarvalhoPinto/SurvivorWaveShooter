using UnityEngine;

public class RiseState : EnvironmentInteractionState
{
    public RiseState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate)
        : base(context, estate)
    {
    }

    public override void EnterState()
    {
        // TODO: Implementar lógica ao entrar no estado de subida.
    }

    public override void ExitState()
    {
        // TODO: Implementar lógica ao sair do estado de subida.
    }

    public override void UpdateState()
    {
        // TODO: Atualizar lógica de subida a cada frame.
    }

    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
    {
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // TODO: Tratar trigger ao iniciar a subida.
    }

    public override void OnTriggerStay(Collider other)
    {
        // TODO: Tratar trigger enquanto estiver subindo.
    }

    public override void OnTriggerExit(Collider other)
    {
        // TODO: Tratar trigger ao sair do estado de subida.
    }
}
