using Godot;
using System;

public partial class HitBox : Area2D
{
	[Signal]
	public delegate void DamageEventHandler(int damage);

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TakeDamage(int damage)
	{
		GD.Print("Take Damage: ", damage);
		EmitSignal(SignalName.Damage, damage);
	}
}
