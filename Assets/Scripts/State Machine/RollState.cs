using UnityEngine;

public class RollState : BaseState
{
    private float acceleration;
    private Vector3 Direction;
    private float speed;

    public RollState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
        InitSubStates();
    }

    protected override void OnStateEnter()
    {
        acceleration = mainPlayer.LastAcceleration <= 0.5f ? 0.5f : mainPlayer.LastAcceleration;
        //Direction = mainPlayer.LastDirection;
        Direction = mainPlayer.CalculateAngledDirection();
        speed = mainPlayer.Speed * 2;
        mainPlayer.CanMove = false;

        var state = mainPlayer.Animancer.Play(mainPlayer.roll, 0.2f);
        state.Time = 0.0f;
        state.Speed = 0.8f;
        state.Events.Add(0.65f, OnRollEnd);
    }

    protected override void OnStateExit()
    {
        mainPlayer.CanMove = true;
        mainPlayer.CurrentAngle = mainPlayer.transform.eulerAngles.y;
    }

    protected override void OnStateUpdate()
    {
        mainPlayer.CharacterController.Move(Direction * (speed * acceleration * Time.deltaTime));
        mainPlayer.transform.forward = Vector3.Lerp(mainPlayer.transform.forward, Direction, 20f * Time.deltaTime);
    }

    protected override void CheckState()
    {
    }

    protected sealed override void InitSubStates()
    {
    }

    private void OnRollEnd()
    {
        ChangeState(mainPlayer.Factory.GroundState());
    }
}