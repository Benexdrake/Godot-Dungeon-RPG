using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    private Node currentState;
    private Node[] states;

    public override void _Ready()
    {
        states = GetChildren().ToArray();

        currentState = states[0];

        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        var newState = states.First(x => x is T);

        if(newState == null)
            return;

        if(currentState is T)
            return;
        

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
