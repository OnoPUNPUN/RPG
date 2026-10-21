using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStateMachine : Node
{
	private List<State> states;
	private State PreviousState;
	private State CurrentState;


	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Disabled;
	}


	public override void _Process(double delta)
	{
		ChangeState(CurrentState.Process(delta));
	}

	public override void _PhysicsProcess(double delta)
	{
		ChangeState(CurrentState.Physics(delta));
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		ChangeState(CurrentState.HandleInput(@event));
	}

	public void Initialize(Player _player)
	{
		states = [];

		foreach (Node c in GetChildren())
		{
			if (c is State s)
			{
				s.player = _player;
				states.Add(s);
			}
		}

		if (states.Count > 0)
		{
			ChangeState(states[0]);
			ProcessMode = ProcessModeEnum.Inherit;
		}
	}

	public void ChangeState(State NewState)
	{
		if (NewState == null || NewState == CurrentState)
		{
			return;
		}

		if (CurrentState != null)
		{
			CurrentState.Exit();
		}

		PreviousState = CurrentState;
		CurrentState = NewState;
		CurrentState.Enter();
	}
}
