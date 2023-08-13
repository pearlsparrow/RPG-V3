public class MoveState : BaseState
{
    public MoveState(MainPlayer demoStateMachine) : base(demoStateMachine)
    {
        RootState = false;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
        mainPlayer.AccelarationClamp = 1.0f;
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }

    protected override void CheckState()
    {
        if (mainPlayer.CurrentAccelaration <= 0)
        {
            ChangeState(mainPlayer.Factory.IdleState());
        }

        if (mainPlayer.InputHandler.IsRunButtonPressed)
        {
            ChangeState(mainPlayer.Factory.RunState());
        }
    }

    protected sealed override void InitSubStates()
    {
    }
}