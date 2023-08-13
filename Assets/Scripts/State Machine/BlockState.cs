public class BlockState : BaseState
{
    public BlockState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
    }

    protected override void CheckState()
    {
        if (!mainPlayer.InputHandler.IsShieldButtonPressed && !verified)
        {
            ChangeState(mainPlayer.Factory.GroundState());
            verified = true;
            //mainPlayer.Speed = 2f;
            //var state = mainPlayer.Animancer.Play(mainPlayer.BlockOut, 0.2f);
            //state.Events.Add(1, BlockOutEnded);
        }

        if (mainPlayer.InputHandler.AttackButtonPressed)
        {
            ChangeState(mainPlayer.Factory.AttackState());
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.AccelarationClamp = 1.0f;
        //mainPlayer.Speed = 2f;
        //var state = mainPlayer.Animancer.Play(mainPlayer.BlockIn, 0.2f);
        //state.Events.Add(1, BlockInEnded);

        mainPlayer.Speed = mainPlayer.MaxSpeed;
        var state = mainPlayer.Animancer.Play(mainPlayer.blendShield, 0.25f);
        mainPlayer.State = mainPlayer.blendShield.State;
    }

    protected override void OnStateExit()
    {
    }

    protected override void OnStateUpdate()
    {
        CheckState();
    }

    private void BlockInEnded()
    {
        mainPlayer.Speed = mainPlayer.MaxSpeed;
        var state = mainPlayer.Animancer.Play(mainPlayer.blendShield, 0.2f);
        mainPlayer.State = mainPlayer.blendShield.State;
    }

    private void BlockOutEnded()
    {
        mainPlayer.Speed = mainPlayer.MaxSpeed;
        ChangeState(mainPlayer.Factory.GroundState());
    }
}