using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private PlayerBase _playerBase;
	
		public static GameManager Instance;

		public GameState State;

		public static event Action<GameState> OnGameStateChanged;
	
	
		private void Awake()
		{
			Instance = this;
			_playerBase.OnPlayerBaseDestroyed += UpdateGameState;
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

	
		public void QuitToMainMenu()
		{
			SceneManager.LoadScene(0);
		}
	
	
		public void LoadEndOfLevelScene()
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