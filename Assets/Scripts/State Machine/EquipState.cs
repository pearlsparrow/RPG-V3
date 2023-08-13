using UnityEngine;

public class EquipState : BaseState
{
    private bool _finished = false;

    public EquipState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
    }

    protected override void CheckState()
    {
        if (_finished)
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
        mainPlayer.Equiped = true;
        var state = mainPlayer.Animancer.Play(mainPlayer.equip, 0.2f);
        state.Events.Add(0.150f, GrabItems);
        state.Events.Add(0.55f, Finished);
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
        _finished = true;
    }

    private void GrabItems()
    {
        mainPlayer.leftHandItem.transform.SetParent(mainPlayer.leftHandEquipedSlot.transform, false);
        mainPlayer.rightHandItem.transform.SetParent(mainPlayer.rightHandEquipedSlot.transform, false);
        mainPlayer.leftHandItem.transform.localPosition = Vector3.zero;
        mainPlayer.rightHandItem.transform.localPosition = Vector3.zero;
    }
}