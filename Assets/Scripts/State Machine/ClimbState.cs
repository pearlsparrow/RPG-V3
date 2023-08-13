using UnityEngine;

public class ClimbState : BaseState
{
    public ClimbState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
        InitSubStates();
    }

    protected override void CheckState()
    {
        if (mainPlayer.CanGoBackToGround)
        {
            ChangeState(mainPlayer.Factory.GroundState());
            mainPlayer.CanGoBackToGround = false;
        }
    }

    protected sealed override void InitSubStates()
    {
        SetSubState(mainPlayer.Factory.GroundToClimbState());
    }

    protected override void OnStateEnter()
    {
        Debug.Log("CLIMB STATE");
    }

    protected override void OnStateExit()
    {
        Debug.Log("CLIMB STATE EXIT");
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }
}