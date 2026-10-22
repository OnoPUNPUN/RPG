using System;
using Godot;

public partial class StateAttack : State
{

	private State walk;
	private State idle;
	private bool attacking = false;
	[Export]
	private AudioStream attackSound;

	[Export(PropertyHint.Range, "1,20,0.5")]
	private float _decelerateSpeed = 5.0f;
	private AudioStreamPlayer2D audio;

	private AnimationPlayer animationPlayer;
	private AnimationPlayer attackAnimation;

    public override void _Ready()
    {
        
		walk = GetNode<State>("../Walk");
		idle = GetNode<State>("../Idle");
		animationPlayer = GetNode<AnimationPlayer>("../../AnimationPlayer");
		attackAnimation = GetNode<AnimationPlayer>("../../Sprite2D/AttackEffectSprite/AttackAnimationPlayer");
		audio = GetNode<AudioStreamPlayer2D>("../../Audio/AudioStreamPlayer2D");
    }

	public override void Enter()
	{
		player.UpdateAnimation("attack");
		attackAnimation.Play("attack_" + player.AnimeDirection());
		attacking = true;
		animationPlayer.AnimationFinished += EndAttack;
		audio.Stream = attackSound;
		audio.Play();
	}

	public override void Exit()
	{
		animationPlayer.AnimationFinished -= EndAttack;
		attacking = false;
	}

	public override State Process(double _delta)
	{
		player.Velocity -= player.Velocity * _decelerateSpeed * (float)_delta;
		if(attacking == false)
		{
			if(player.Direction  == Vector2.Zero)
			{
				return idle;
			} 
			else
			{
				return walk;
			}
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

	private void EndAttack(StringName _newAnimation)
	{
		attacking = false;
	}
}
