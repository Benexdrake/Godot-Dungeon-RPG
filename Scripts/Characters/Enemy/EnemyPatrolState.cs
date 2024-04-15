using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range,"0,20,0.5")] private float maxIdleTime = 4f;
    private int PointIndex = 0;
    protected override void EnterState()
    {
        PointIndex = 1;
        destination = GetPointGlobalPosition(PointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;

        idleTimerNode.Timeout +=HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if(!idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
        base._PhysicsProcess(delta);
    }

    protected override void ExitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;

        idleTimerNode.Timeout -=HandleTimeout;

        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        var rng = new RandomNumberGenerator();
        idleTimerNode.WaitTime = rng.RandfRange(0,maxIdleTime);

        idleTimerNode.Start();

        PointIndex = Mathf.Wrap(
            PointIndex +1,
            0,
            characterNode.PathNode.Curve.PointCount
        );
        destination = GetPointGlobalPosition(PointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }

    private void HandleTimeout()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        PointIndex = Mathf.Wrap(
            PointIndex +1,
            0,
            characterNode.PathNode.Curve.PointCount
        );
        destination = GetPointGlobalPosition(PointIndex);
    }
}
