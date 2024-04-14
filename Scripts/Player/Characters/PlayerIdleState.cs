using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if(!characterNode.IsOnFloor())
        {
            GD.Print("Test");
            Vector3 velocity = characterNode.Velocity;
            velocity.Y -= 9.8f;
            characterNode.Velocity = velocity;
        }
        if(characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.INPUTS_Dash))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
