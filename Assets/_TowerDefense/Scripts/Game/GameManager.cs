using System;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private PlayerBase _playerBase;
		[SerializeField] private EnemyManager _enemyManager;
	
		public static GameManager Instance;

		public GameState State;

		public static event Action<GameState> OnGameStateChanged;
	
	
		private void Awake()
		{
			Instance = this;
			_playerBase.OnPlayerBaseDestroyed += UpdateGameState;
			_enemyManager.EnemyWaveCompleted += UpdateGameState;
		}
	

		private void Start()
		{
			UpdateGameState(GameState.TowerPlacement);
		}
	

		public void UpdateGameState(GameState newState)
		{
			State = newState;
			
			switch (State)
			{
				case GameState.TowerPlacement:
					break;
				case GameState.EnemyWave:
					break;
				case GameState.Victory:
					LoadEndOfLevelScene();
					break;
				case GameState.Lose:
					LoadEndOfLevelScene();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
			}
		
			OnGameStateChanged?.Invoke(newState);
		}

		
		private void QuitToMainMenu()
		{
			SceneManager.LoadScene(0);
		}
	
	
		private void LoadEndOfLevelScene()
		{
			SceneManager.LoadScene(2);
		}
	}

	
	public enum GameState
	{
		TowerPlacement,
		EnemyWave,
		Victory,
		Lose
	}
}