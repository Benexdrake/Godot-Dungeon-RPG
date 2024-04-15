using Godot;
public abstract partial class CharacterState : Node
{
    protected Character characterNode;
     
    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        CheckFloor();
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if(what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if(what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);

            ExitState();
        }
    }

    protected void CheckFloor()
    {
        if(!characterNode.IsOnFloor())
        {
            Vector3 velocity = characterNode.Velocity;
            velocity.Y -= 9.8f;
            characterNode.Velocity = velocity;
        }
    }

    protected virtual void EnterState()
    {}

    protected virtual void ExitState()
    {}
}