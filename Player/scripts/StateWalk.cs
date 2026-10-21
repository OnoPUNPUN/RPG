using Godot;

public partial class StateWalk : State
{
	[Export]
	float MoveSpeed = 100.0f;

	private State idle;

	public override void _Ready()
	{
		idle = GetNode<State>("../Idle");
	}
	public override void Enter()
	{
		player.UpdateAnimation("walk");
	}

	public override void Exit()
	{
	}

	public override State Process(double delta)
	{
		if (player.Direction == Vector2.Zero)
		{
			return idle;
		}

		player.Velocity = player.Direction * MoveSpeed;

		if (player.SetDirection())
		{
			player.UpdateAnimation("walk");
		}

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
