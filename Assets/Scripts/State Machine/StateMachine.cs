public class StateMachine
{
    private MainPlayer demoStateMachine;
    public BaseState currentState;

    //Constructor
    public StateMachine(MainPlayer demoStateMachine)
    {
        this.demoStateMachine = demoStateMachine;
        currentState = demoStateMachine.Factory.GroundState();
    }

    public void ChangeState(BaseState state)
    {
        currentState = state;
    }
}