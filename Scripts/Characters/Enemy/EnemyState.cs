using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    protected Vector3 GetPointGlobalPosition(int index)
    {
        var localPosition = characterNode.PathNode.Curve.GetPointPosition(index);
        var  globalPos = characterNode.PathNode.GlobalPosition;
        return localPosition + globalPos;
    }

    protected void Move()
    {
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }
}