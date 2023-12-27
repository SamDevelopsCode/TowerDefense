using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		UpdateGameState(GameState.TowerPlacement);
	}
	

	public void UpdateGameState(GameState newState)
	{
		State = newState;

		switch (newState)
		{
			case GameState.TowerPlacement:
				break;
			case GameState.EnemyWave:
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

	public void QuitToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
	
}

public enum GameState
{
	TowerPlacement,
	EnemyWave,
	Victory,
	Lose
}
