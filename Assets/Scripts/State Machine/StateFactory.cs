public class StateFactory
{
    MainPlayer demoStateMachine;

    public StateFactory(MainPlayer demoStateMachine)
    {
        this.demoStateMachine = demoStateMachine;
    }

    public BaseState IdleState()
    {
        return new IdleState(demoStateMachine);
    }

    public BaseState GroundState()
    {
        return new GroundState(demoStateMachine);
    }

    public BaseState JumpState()
    {
        return new JumpState(demoStateMachine);
    }

    public BaseState WaterState()
    {
        return new WaterState(demoStateMachine);
    }

    public BaseState MoveState()
    {
        return new MoveState(demoStateMachine);
    }

    public BaseState FallState()
    {
        return new FallState(demoStateMachine);
    }

    public BaseState AttackState()
    {
        return new AttackState(demoStateMachine);
    }

    public BaseState RunState()
    {
        return new RunState(demoStateMachine);
    }

    public BaseState ClimbState()
    {
        return new ClimbState(demoStateMachine);
    }

    public BaseState ClimbingState()
    {
        return new ClimbingState(demoStateMachine);
    }

    public BaseState ClimbToGroundState()
    {
        return new ClimbToGroundState(demoStateMachine);
    }

    public BaseState GroundToClimbState()
    {
        return new GroundToClimbState(demoStateMachine);
    }

    public BaseState EquipState()
    {
        return new EquipState(demoStateMachine);
    }

    public BaseState UnequipState()
    {
        return new UnequipState(demoStateMachine);
    }

    public BaseState BlockState()
    {
        return new BlockState(demoStateMachine);
    }

    public BaseState RollState()
    {
        return new RollState(demoStateMachine);
    }
}