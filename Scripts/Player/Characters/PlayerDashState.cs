using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player characterNode;
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 20;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        dashTimerNode.Timeout += HandleDashTimeout;
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if(what == GameConstants.STATE_NOTIFICATION_ENABLE)
        {
            characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);

            characterNode.Velocity = new(characterNode.direction.X,0,characterNode.direction.Y);

            if(characterNode.Velocity == Vector3.Zero)
            {
                characterNode.Velocity = characterNode.spriteNode.FlipH ? Vector3.Left : Vector3.Right;
            }

            characterNode.Velocity *= speed;

            dashTimerNode.Start();

            SetPhysicsProcess(true);
        }
        else if(what == GameConstants.STATE_NOTIFICATION_DISABLE)
        {
            SetPhysicsProcess(false);
        }
    }

    private void HandleDashTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.stateMachineNode.SwitchState<PlayerIdleState>();
    }
}
