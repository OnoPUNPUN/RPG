using Godot;
using System;

public partial class Plant : Node2D
{
    private HitBox hitBox;

    public override void _Ready()
    {
        hitBox = GetNode<HitBox>("HitBox");
        hitBox.Damage += TakeDamage;
    }

    public void TakeDamage(int damage)
    {
        QueueFree();
    }
}
