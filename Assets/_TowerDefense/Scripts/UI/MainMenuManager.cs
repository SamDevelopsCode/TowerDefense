using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup LevelSelect;

        public void LoadLevel1()
        {
            SceneManager.LoadScene(1);
        }

    
        public void ShowLevelSelectionMenu()
        {
            LevelSelect.alpha = 1;
            LevelSelect.blocksRaycasts = true;
        }

    
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
