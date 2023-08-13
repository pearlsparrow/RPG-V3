using UnityEngine;

public class JumpState : BaseState
{
    bool landingEnded = false;

    public JumpState(MainPlayer demoStateMachine) : base(demoStateMachine)
    {
        RootState = true;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
        mainPlayer.Animancer.Play(mainPlayer.jump, 0.1f);
        mainPlayer.CurrentGravity = mainPlayer.Gravity;
        mainPlayer.DirectionY = Mathf.Sqrt(-2 * -mainPlayer.CurrentGravity * mainPlayer.JumpHeigh);
    }

    protected override void OnStateExit()
    {
        if (mainPlayer.CurrentAccelaration > 1f) mainPlayer.CurrentAccelaration = 1f;
    }

    protected override void OnStateUpdate()
    {
        if (mainPlayer.DirectionY < 0) mainPlayer.CurrentGravity = mainPlayer.JumpFallGravity;

        mainPlayer.DirectionY -= mainPlayer.CurrentGravity * Time.deltaTime;

        if (mainPlayer.IsGrounded && !verified)
        {
            verified = true;
            var state = mainPlayer.Animancer.Play(mainPlayer.landing, 0.1f);
            state.Events.Add(0.21f, () => landingEnded = true);
        }

        CheckState();
    }

    protected override void CheckState()
    {
        if (mainPlayer.IsGrounded && mainPlayer.DirectionY < 0 && landingEnded)
        {
            ChangeState(mainPlayer.Factory.GroundState());
        }
    }

    protected sealed override void InitSubStates()
    {
    }

    public void AnimatorUpdate()
    {
    }
}