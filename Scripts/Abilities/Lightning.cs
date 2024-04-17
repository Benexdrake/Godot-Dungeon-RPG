using Godot;
using System;

public partial class Lightning : Ability
{
    [Export] public float Damage { get; set; } = 10;

    public override void _Ready()
    {
        playerNode.AnimationFinished += (animeName) => QueueFree();
    }

}
