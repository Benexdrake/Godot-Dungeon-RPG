using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export(PropertyHint.Range,"0,50,0.5")] private float speed = 20;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X,0,characterNode.direction.Y);
        if(characterNode.Velocity == Vector3.Zero)
        {
            characterNode.Velocity = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        }
        characterNode.Velocity *= speed;
        dashTimerNode.Start();        
    }

    private void HandleDashTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}
