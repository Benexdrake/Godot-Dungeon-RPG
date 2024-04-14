    using Godot;
    using System;
     
    public partial class FallState : PlayerState
    {
        [Export] private Vector3 gravity = new(0, -1.2f, 0);
     
        public override void _PhysicsProcess(double delta)
        {
            characterNode.Velocity += gravity;
            characterNode.MoveAndSlide();
     
            if(characterNode.IsOnFloor())
            {
                characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            }
        }
     
        protected override void EnterState()
        {
            characterNode?.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        }
    }