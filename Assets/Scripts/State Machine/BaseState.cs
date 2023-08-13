using UnityEngine;

public abstract class BaseState
{
    private BaseState superState;
    private BaseState subState;
    protected bool RootState;
    protected MainPlayer mainPlayer;
    protected bool verified;
    protected string name;

    protected BaseState(MainPlayer mainPlayer)
    {
        this.mainPlayer = mainPlayer;
    }

    protected abstract void OnStateEnter();
    protected abstract void OnStateExit();
    protected abstract void OnStateUpdate();
    protected abstract void CheckState();
    protected abstract void InitSubStates();

    public void OnStatesUpdate()
    {
        //Updates the main state
        OnStateUpdate();
        //If the main state has a substates - Update that also
        subState?.OnStatesUpdate();
    }

    public void OnStatesEnter()
    {
        this.OnStateEnter();
        subState?.OnStatesEnter();
    }

    private void OnStatesExit()
    {
        this.OnStateExit();
        subState?.OnStatesExit();
    }

    protected void ChangeState(BaseState newState)
    {
        verified = true;

        //Exit current State
        this.OnStatesExit();

        //Enter New State
        newState.OnStatesEnter();

        //If it is a Root state then change the state with a Root one
        if (RootState)
        {
            mainPlayer.StateMachine.ChangeState(newState);
        }
        //If this isn't a Root state and the superState is not null(that means that the state that called this function is a substate) 
        //Then just swap the substate
        else
        {
            superState?.SetSubState(newState);
        }
    }

    protected void SetSubState(BaseState newState)
    {
        this.subState = newState;
        newState.SetSuperState(this);
    }

    private void SetSuperState(BaseState newState)
    {
        this.superState = newState;
        KaraflaStinMani();
    }

    private void KaraflaStinMani()
    {
        Physics.Raycast(new Vector3(1, 1, 1), new Vector3(2, 3, 1), 2.3f);
    }
}