public class IdleState : BaseState
{
    public IdleState(MainPlayer demoStateMachine) : base(demoStateMachine)
    {
        RootState = false;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
        mainPlayer.DirectionY = mainPlayer.DownForce;
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
        if (mainPlayer.CurrentAccelaration > 0 && mainPlayer.IsGrounded)
        {
            ChangeState(mainPlayer.Factory.MoveState());
        }
    }

    protected sealed override void InitSubStates()
    {
    }
}