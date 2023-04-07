using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private int _playLevelIndex = 1;

    public void PlayButton()
    {
        SceneManager.LoadScene(_playLevelIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    
}
