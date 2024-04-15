using Godot;
using System;

public partial class Player : Character
{
    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUTS_MOVE_LEFT, GameConstants.INPUTS_MOVE_RIGHT, 
            GameConstants.INPUTS_MOVE_FORWARD, GameConstants.INPUTS_MOVE_BACKWARD
        );
    }
}
