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

        base._PhysicsProcess(delta);
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();

        CheckForDashInput();
    }

        protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
