using UnityEngine;

public class RunState : BaseState
{
    public RunState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = false;
        InitSubStates();
    }

    protected override void CheckState()
    {
        if (mainPlayer.InputHandler.IsRunButtonPressed == false)
        {
            ChangeState(mainPlayer.Factory.MoveState());
        }
    }

    protected sealed override void InitSubStates()
    {
        Debug.Log("This is a " + GetType());
    }

    protected override void OnStateEnter()
    {
        mainPlayer.AccelarationClamp = 1.5f;
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }
}