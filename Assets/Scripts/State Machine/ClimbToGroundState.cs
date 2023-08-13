public class ClimbToGroundState : BaseState
{
    public ClimbToGroundState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = false;
    }

    protected override void CheckState()
    {
        if (verified) return;
        if (mainPlayer.isGrounded)
        {
            mainPlayer.CanGoBackToGround = true;
            verified = true;
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }
}