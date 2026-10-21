using Godot;
using System;
using System.Data;

public partial class Player : CharacterBody2D
{
	Vector2 CardinalDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;
	private PlayerStateMachine stateMachine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<Sprite2D>("Sprite2D");
		stateMachine = GetNode<PlayerStateMachine>("SateMachine");

		stateMachine.Initialize(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Direction = new Vector2(
			Input.GetAxis("left", "right"),
			Input.GetAxis("up", "down")
		).Normalized();
	}


	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public bool SetDirection()
	{
		Vector2 NewDirection = CardinalDirection;

		if (Direction == Vector2.Zero)
		{
			return false;
		}

		if (Direction.Y == 0)
		{
			NewDirection = Direction.X < 0 ? Vector2.Left : Vector2.Right;
		}
		else if (Direction.X == 0)
		{
			NewDirection = Direction.Y < 0 ? Vector2.Up : Vector2.Down;
		}


		if (NewDirection == CardinalDirection)
		{
			return false;
		}

		CardinalDirection = NewDirection;

		sprite.Scale = new Vector2(CardinalDirection == Vector2.Left ? -1 : 1, sprite.Scale.Y);

		return true;
	}

	public void UpdateAnimation(string state)
	{
		animationPlayer.Play(state + "_" + AnimeDirection());
	}

	public String AnimeDirection()
	{
		if (CardinalDirection == Vector2.Down)
		{
			return "down";
		}
		else if (CardinalDirection == Vector2.Up)
		{
			return "up";
		}
		else
		{
			return "side";
		}
	}

}
