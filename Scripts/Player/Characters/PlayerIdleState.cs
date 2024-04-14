using Godot;
using System;

public partial class PlayerIdleState : Node
{
    private Player characterNode;
    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if(characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if(what == GameConstants.STATE_NOTIFICATION_ENABLE)
        {
            characterNode.animPlayerNode.Play(GameConstants.ANIM_IDLE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if(what == GameConstants.STATE_NOTIFICATION_DISABLE)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.INPUTS_Dash))
        {
            characterNode.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
