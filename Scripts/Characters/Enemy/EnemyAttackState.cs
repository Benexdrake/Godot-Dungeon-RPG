using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;

    public override void _Ready()
    {
        Ready();
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);

        var target = characterNode.AttackAreaNode.GetOverlappingBodies().First();

        targetPosition = target.GlobalPosition;

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitbox(true);

        var target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            var chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();

            if(chaseTarget == null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
            }

            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        var direction =characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.SpriteNode.FlipH = direction.X < 0;
    }

    private void PerformHit()
    {
        characterNode.ToggleHitbox(false);
        characterNode.HitboxNode.GlobalPosition = targetPosition;
    }
}
