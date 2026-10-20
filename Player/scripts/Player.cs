using Godot;
using System;
using System.Data;

public partial class Player : CharacterBody2D
{
	const float MoveSpeed = 100.0f;
	Vector2 CardinalDirection = Vector2.Down;
	Vector2 Direction = Vector2.Zero;
	String State = "idle";
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Direction.X = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		Direction.Y = Input.GetActionStrength("down") - Input.GetActionStrength("up");

		Velocity = Direction * MoveSpeed;

		if(SetState() || SetDirection())
		{
			UpdateAnimation();
		}
		
	}


	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public bool SetDirection()
	{
		Vector2 NewDirection = CardinalDirection;

		if(Direction == Vector2.Zero)
		{
			return false;
		}
		
		if(Direction.Y == 0)
		{
			NewDirection = Direction.X < 0 ? Vector2.Left : Vector2.Right;
		}
		else if(Direction.X == 0)
		{
			NewDirection = Direction.Y < 0 ? Vector2.Up : Vector2.Down;
		}


		if(NewDirection == CardinalDirection)
		{
			return false;
		}

		CardinalDirection = NewDirection;

		sprite.Scale = new Vector2(CardinalDirection == Vector2.Left ? -1 : 1, sprite.Scale.Y);
		
		return true;
	}

	public bool SetState()
	{
		String NewState = Direction == Vector2.Zero ? "idle" : "walk";

		if(NewState == State)
		{
			return false;
		}

		State = NewState;

		return true;
	}

	public void UpdateAnimation()
	{
		animationPlayer.Play(State + "_" + AnimeDirection());
	}

	public String AnimeDirection()
	{
		if(CardinalDirection == Vector2.Down)
		{
			return "down";
		} else if (CardinalDirection == Vector2.Up)
		{
			return "up";
		} else
		{
			return "side";
		}
	}

}
