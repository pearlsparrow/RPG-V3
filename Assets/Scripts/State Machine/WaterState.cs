public class WaterState : BaseState
{
    public WaterState(MainPlayer demoStateMachine) : base(demoStateMachine)
    {
        RootState = false;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
    }

    protected override void CheckState()
    {
    }

    protected sealed override void InitSubStates()
    {
    }
}