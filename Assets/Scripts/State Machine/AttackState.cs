using UnityEngine;

public class AttackState : BaseState
{
    private bool nextAttack;
    private int index;
    private bool comboFinished;

    public AttackState(MainPlayer mainPlayer) : base(mainPlayer)
    {
        RootState = true;
    }


    protected override void CheckState()
    {
        if (mainPlayer.AttackEnded1 && !mainPlayer.NextAttack)
        {
            ChangeState(mainPlayer.Factory.GroundState());
        }
    }

    protected override void InitSubStates()
    {
    }

    protected override void OnStateEnter()
    {
        mainPlayer.Speed = 0.8f;

        var state = mainPlayer.Animancer.Play(mainPlayer.attacks[index], 0.1f);
        state.Time = 0.0f;
        state.Events.Add(0.5f, () => mainPlayer.Speed = 0f);
        state.Events.Add(0.8f, OnAttackEnd);
    }

    protected override void OnStateExit()
    {
        mainPlayer.NextAttack = false;
        mainPlayer.AttackEnded1 = false;
        mainPlayer.Speed = mainPlayer.MaxSpeed;
    }

    protected override void OnStateUpdate()
    {
        //if(mainPlayer.InputHandler.AttackButtonPressed) { mainPlayer.NextAttack = true; mainPlayer.Animator.SetTrigger("Attack"); }
        if (mainPlayer.InputHandler.AttackButtonPressed)
        {
            if (index == mainPlayer.attacks.Length - 1)
            {
                nextAttack = false;
            }
            else
            {
                nextAttack = true;
            }
        }

        CheckState();
    }

    private void OnAttackEnd()
    {
        Debug.Log("EVENT FIRED");
        if (nextAttack)
        {
            index++;
            nextAttack = false;
            mainPlayer.Speed = 0.8f;
            var state = mainPlayer.Animancer.Play(mainPlayer.attacks[index], 0.1f);
            state.Time = 0.0f;
            state.Events.Add(0.5f, () => mainPlayer.Speed = 0f);
            state.Events.Add(0.8f, OnAttackEnd);
        }
        else
        {
            mainPlayer.AttackEnded1 = true;
        }
    }
}