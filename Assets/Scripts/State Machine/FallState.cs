using UnityEngine;

public class FallState : BaseState
{
    public FallState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
        InitSubStates();
    }

    protected override void CheckState()
    {
        if (mainPlayer.IsGrounded)
        {
            ChangeState(mainPlayer.Factory.GroundState());
        }
    }

    protected sealed override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.CurrentGravity = mainPlayer.Gravity;
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
        mainPlayer.DirectionY -= mainPlayer.CurrentGravity * Time.deltaTime;
    }
}