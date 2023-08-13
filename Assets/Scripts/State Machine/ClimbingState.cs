public class ClimbingState : BaseState
{
    public ClimbingState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = false;
    }

    protected override void CheckState()
    {
        if (mainPlayer.TrackWalls.CanGoBackToGround() && mainPlayer.InputHandler.RawInput.z < 0)
        {
            ChangeState(mainPlayer.Factory.ClimbToGroundState());
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.InputHandler.InputEnabled = true;
        mainPlayer.CanClimb = true;
    }

    protected override void OnStateExit()
    {
        mainPlayer.CanClimb = false;
        mainPlayer.InputHandler.InputEnabled = true;
        mainPlayer.CanMove = true;
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }
}