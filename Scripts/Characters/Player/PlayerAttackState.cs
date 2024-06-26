using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode; 
    [Export] private PackedScene lightningScene; 
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        Ready();
        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        PlayAnimation(comboCounter);

        characterNode.HitboxNode.BodyEntered += HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if(comboCounter != maxComboCount)
            return;

        var lightning = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;
    }


    private void HandleAnimationFinished(StringName animName)
    {
        if(comboCounter < maxComboCount)
            comboCounter++;
        else
            comboCounter = 1;

        characterNode.ToggleHitbox(true);
        
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _PhysicsProcess(double delta)
    {
        CheckFloor();
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        
        characterNode.HitboxNode.BodyEntered -= HandleBodyEntered;
        comboTimerNode.Start();
    }

    private void PlayAnimation(int combo)
    {
        string animation = string.Empty;
        switch (combo)
        {
            case 1:
                animation = GameConstants.ANIM_KICK;
                break;
            case 2:
                animation = GameConstants.ANIM_ATTACK;
                break;
        }
        characterNode.AnimPlayerNode.Play(animation, customSpeed: 2f);
    }

    private void PerformHit()
    {
        var newPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        characterNode.HitboxNode.Position = newPosition;
        characterNode.ToggleHitbox(false);
    }
}
