using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.STATE_NOTIFICATION_ENABLE);
    }

    public void SwitchState<T>()
    {
        Node newState = null;

        foreach (Node state in states)
        {
            if(state is T)
            {
                newState = state;
            }
        }

        if(newState == null)
            return;

        currentState.Notification(GameConstants.STATE_NOTIFICATION_DISABLE);
        currentState = newState;
        currentState.Notification(GameConstants.STATE_NOTIFICATION_ENABLE);
    }
}
