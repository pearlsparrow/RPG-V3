using UnityEngine;

public class UnequipState : BaseState
{
    bool finished = false;

    public UnequipState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
    }

    protected override void CheckState()
    {
        if (finished)
        {
            ChangeState(mainPlayer.Factory.GroundState());
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.Speed = 1.0f;
        mainPlayer.InputHandler.InputEnabled = false;
        mainPlayer.Equiped = false;
        var state = mainPlayer.Animancer.Play(mainPlayer.unequip, 0.2f);
        state.Events.Add(0.51f, LeaveItems);
        state.Events.Add(0.57f, Finished);
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }

    private void Finished()
    {
        finished = true;
    }

    private void LeaveItems()
    {
        mainPlayer.leftHandItem.transform.SetParent(mainPlayer.leftHandUnequipedSlot.transform, false);
        mainPlayer.rightHandItem.transform.SetParent(mainPlayer.rightHandUnequipedSlot.transform, false);
        mainPlayer.leftHandItem.transform.localPosition = Vector3.zero;
        mainPlayer.rightHandItem.transform.localPosition = Vector3.zero;
    }
}