using Godot;
using System;

public partial class PlayerInteractionsHost : Node2D
{
	
	private Player player;
	public override void _Ready()
	{
		player = GetNode<Player>("..");
		player.DirectionChanged += UpdateDirection;
	}


	public void UpdateDirection(Vector2 newDirection)
	{
		if (newDirection == Vector2.Down)
			RotationDegrees = 0;
		else if (newDirection == Vector2.Up)
			RotationDegrees = 180;
		else if (newDirection == Vector2.Left)
			RotationDegrees = 90;
		else if (newDirection == Vector2.Right)
			RotationDegrees = -90;
		else
			RotationDegrees = 0;
	}
}
