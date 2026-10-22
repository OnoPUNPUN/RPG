using Godot;
using System;

public partial class State : Node
{

	public Player player; // Ref of Our Player


	public override void _Ready()
	{
	}

	// What heppens When PLayer Enter this State?
	public virtual void Enter() { }

	public virtual void Exit() { }

	public virtual State Process(double delta)
	{
		return null;
	}

	public virtual State Physics(double delta)
	{
		return null;
	}

	public virtual State HandleInput(InputEvent _event)
	{
		return null;
	}

}
