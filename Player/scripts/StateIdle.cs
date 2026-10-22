using Godot;

public partial class StateIdle : State
{

	private State walk;
	private State attack;

    public override void _Ready()
    {
        walk = GetNode<State>("../Walk");
		attack = GetNode<State>("../Attack");
    }

	public override void Enter()
	{
		player.UpdateAnimation("idle");
		
	}

	public override void Exit()
	{
	}

	public override State Process(double delta)
	{
		if (player.Direction != Vector2.Zero)
		{
			return walk;
		}
		player.Velocity = Vector2.Zero;
		return null;
	}

	public override State Physics(double delta)
	{
		return null;
	}

	public override State HandleInput(InputEvent _event)
	{
		if(_event.IsActionPressed("attack"))
		{
			return attack;
		}
		return null;
	}
}
