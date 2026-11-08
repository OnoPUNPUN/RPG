using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class Player : CharacterBody2D
{
	Vector2 CardinalDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;
	private PlayerStateMachine stateMachine;
	private static readonly Vector2[] DIR_4 = { Vector2.Right, Vector2.Down, Vector2.Left, Vector2.Up };

	[Signal]
	public delegate void DirectionChangedEventHandler(Vector2 newDirection);

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
		if (Direction == Vector2.Zero)
		{
			return false;
		}


		// This took long to figure out my player was moon walking when pressing to button Don't dare to touch this code
		float angle = Direction.Angle() + CardinalDirection.Angle() * 0.1f;
		if (angle < 0)
		{
			angle += float.Tau;
		}

		int directionId = (int)Math.Round(angle / float.Tau * DIR_4.Length) % DIR_4.Length;
		var NewDirection = DIR_4[directionId];

		if (NewDirection == CardinalDirection)
		{
			return false;
		}

		CardinalDirection = NewDirection;
		EmitSignal(SignalName.DirectionChanged, NewDirection);
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
