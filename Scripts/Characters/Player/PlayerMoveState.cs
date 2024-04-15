using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range,"0,20,0.5")] public float Speed { get; private set; } = 10;
    public override void _PhysicsProcess(double delta)
    {
        if(characterNode.direction == Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= Speed;

        if(!characterNode.IsOnFloor())
        {
            Vector3 velocity = characterNode.Velocity;
            velocity.Y -= 9.8f;
            characterNode.Velocity = velocity;
        }

        characterNode.MoveAndSlide();
        characterNode.Flip();
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
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
