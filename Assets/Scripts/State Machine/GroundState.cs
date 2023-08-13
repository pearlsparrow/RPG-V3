using UnityEngine;

public class GroundState : BaseState
{
    public GroundState(MainPlayer demoStateMachine) : base(demoStateMachine)
    {
        RootState = true;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
        Debug.Log("GROUND STATE");
        mainPlayer.Speed = mainPlayer.MaxSpeed;

        mainPlayer.InputHandler.InputEnabled = true;
        var anim = mainPlayer.Equiped ? mainPlayer.blendEquiped : mainPlayer.blendUnequiped;
        mainPlayer.Animancer.Play(anim, 0.25f);
        mainPlayer.State = anim.State;
        mainPlayer.DirectionY = mainPlayer.DownForce;
    }

    protected override void OnStateExit()
    {
        Debug.Log("EXIT GROUND STATE");
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }

    protected override void CheckState()
    {
        if (verified) return;
        if (mainPlayer.InputHandler.IsRollButtonPressed && mainPlayer.Equiped &&
            mainPlayer.InputHandler.RawInput.magnitude > 0)
        {
            Debug.Log("ROOLLLL");
            ChangeState(mainPlayer.Factory.RollState());
        }

        if (verified) return;
        if (mainPlayer.TrackWalls.CanClimb())
        {
            Debug.Log("ClimbState");
            ChangeState(mainPlayer.Factory.ClimbState());
        }

        if (verified) return;
        if (mainPlayer.InputHandler.AttackButtonPressed && mainPlayer.Equiped)
        {
            Debug.Log("AttackState");
            ChangeState(mainPlayer.Factory.AttackState());
        }

        if (verified) return;
        if (mainPlayer.InputHandler.IsJumpButtonPressed)
        {
            Debug.Log("JumpState");
            ChangeState(mainPlayer.Factory.JumpState());
        }

        if (verified) return;
        if (mainPlayer.IsGrounded == false)
        {
            Debug.Log("FallState");
            ChangeState(mainPlayer.Factory.FallState());
        }

        if (verified) return;
        if (mainPlayer.InputHandler.IsEquipButtonPressed)
        {
            ChangeState(mainPlayer.Equiped ? mainPlayer.Factory.UnequipState() : mainPlayer.Factory.EquipState());
        }

        if (verified) return;
        if (mainPlayer.InputHandler.IsShieldButtonPressed && mainPlayer.Equiped)
        {
            ChangeState(mainPlayer.Factory.BlockState());
        }
    }

    protected sealed override void InitSubStates()
    {
        SetSubState(mainPlayer.CurrentAccelaration > 0 ? mainPlayer.Factory.MoveState() : mainPlayer.Factory.IdleState());
    }
}