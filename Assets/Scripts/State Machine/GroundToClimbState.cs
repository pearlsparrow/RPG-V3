public class GroundToClimbState : BaseState
{
    bool finishedTransition = true;

    public GroundToClimbState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = false;
    }

    protected override void CheckState()
    {
        if (finishedTransition)
        {
            ChangeState(mainPlayer.Factory.ClimbingState());
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.CanMove = false;
        mainPlayer.InputHandler.InputEnabled = false;
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }
}