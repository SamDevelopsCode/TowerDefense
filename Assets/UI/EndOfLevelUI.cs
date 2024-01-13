using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfLevelUI : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
