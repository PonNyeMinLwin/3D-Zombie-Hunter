using UnityEngine;

public class LandingState : State
{
    private float timePassed;
    private float landingTime;

    public LandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter() 
    {
        base.Enter();

        timePassed = 0f;
        character.animator.SetTrigger("isLanding");
        landingTime = 0.5f;
    }

    public override void LogicUpdate() 
    {
        base.LogicUpdate();

        if (timePassed > landingTime)
        {
            character.animator.SetTrigger("isMoving");
            stateMachine.ChangeState(character.standing);
        }

        timePassed += Time.deltaTime;
    }
}
