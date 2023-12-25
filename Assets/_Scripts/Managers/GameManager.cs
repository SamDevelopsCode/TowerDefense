using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameState State;

	public static event Action<GameState> OnGameStateChanged;
	
	
	private void Awake()
	{
		Instance = this;
	}


	private void Start()
	{
		UpdateGameState(GameState.LevelSelect);
	}


	public void UpdateGameState(GameState newState)
	{
		State = newState;

		switch (newState)
		{
			case GameState.LevelSelect:
				HandleLevelSelect();
				break;
			case GameState.TowerPlacement:
				break;
			case GameState.EnemyWaveIncoming:
				break;
			case GameState.Victory:
				break;
			case GameState.Lose:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
		}
		
		OnGameStateChanged?.Invoke(newState);
	}

	private void HandleLevelSelect()
	{
		
	}
}

public enum GameState
{
	LevelSelect,
	TowerPlacement,
	EnemyWaveIncoming,
	Victory,
	Lose
}
