using System;
using System.Linq;
using Godot;
public abstract partial class Character: CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode {get; private set;}
    [Export] public Sprite3D SpriteNode {get; private set;}
    [Export] public StateMachine StateMachineNode {get; private set;}
    [Export] public Area3D HurtboxNode {get; private set;}
    [Export] public Area3D HitboxNode {get; private set;}
    [Export] public CollisionShape3D HitboxShapeNode {get; private set;}

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();
    public override void _Ready()
    {
        HurtboxNode.AreaEntered += HandleHurtboxEntered;
    }

    private void HandleHurtboxEntered(Area3D area)
    {
        var health = GetStatResource(Stat.Health);
        var player = area.GetOwner<Character>();

        GD.Print(health.StatValue);

        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.First(x => x.StatType == stat);
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if(isNotMovingHorizontally)
        {
            return;
        }

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }

    public void ToggleHitbox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }
}