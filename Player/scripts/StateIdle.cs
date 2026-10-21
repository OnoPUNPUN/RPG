using Godot;

public partial class StateIdle : State
{

	private State walk;
	public override void Enter()
	{
		player.UpdateAnimation("idle");
		walk = GetNode<State>("../Walk");
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

	public override State HandleInput(InputEvent inputEvent)
	{
		return null;
	}
}
