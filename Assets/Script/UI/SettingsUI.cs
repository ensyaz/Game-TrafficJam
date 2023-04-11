using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    #region Settings UI Objects
    [SerializeField]
    private Slider environmentSlider;
    [SerializeField]
    private Slider goldSlider;
    [SerializeField]
    private Slider backgroundAudioSlider;
    [SerializeField]
    private GameObject settingsUI;
    [SerializeField]
    private GameObject player;
    #endregion

    private int _mainMenuIndex = 0;

    private void OnEnable()
    {
        PauseGame();
    }

    private void OnDisable()
    {
        ResumeGame();
    }

    private void Start()
    {
        InitSliders();
        // Set the volume of audio by slider
        environmentSlider.onValueChanged.AddListener((v) =>
        {
            SoundManager.sharedInstance.trafficSoundAudioSource.volume = v;
            SoundManager.sharedInstance.citySoundAudioSource.volume = v;
        });
        // Set the volume of audio by slider
        goldSlider.onValueChanged.AddListener((v) =>
        {
            SoundManager.sharedInstance.goldAudioSource.volume = v;
        });
        // Set the volume of audio by slider
        backgroundAudioSlider.onValueChanged.AddListener((v) =>
        {
            SoundManager.sharedInstance.backgroundAudioSource.volume = v;
        });
    }

    private void InitSliders()
    {
        environmentSlider.value = SoundManager.sharedInstance.trafficSoundAudioSource.volume;
        goldSlider.value = SoundManager.sharedInstance.goldAudioSource.volume;
        backgroundAudioSlider.value = SoundManager.sharedInstance.backgroundAudioSource.volume;
    }
    // Pause the game after settings menu opened
    private void PauseGame()
    {
        Time.timeScale = 0;
        player.GetComponent<InputManager>().enabled = false;
        SoundManager.sharedInstance.citySoundAudioSource.Pause();
        SoundManager.sharedInstance.trafficSoundAudioSource.Pause();
        SoundManager.sharedInstance.backgroundAudioSource.Pause();
    }
    // Resume the game after settings menu closed
    private void ResumeGame()
    {
        Time.timeScale = 1;
        player.GetComponent<InputManager>().enabled = true;
        SoundManager.sharedInstance.citySoundAudioSource.Play();
        SoundManager.sharedInstance.trafficSoundAudioSource.Play();
        SoundManager.sharedInstance.backgroundAudioSource.Play();
    }
    // Settings menu button methods
    public void ReturnButton() => settingsUI.SetActive(false);
    public void ExitButton() => Application.Quit();
    public void MainMenuButton() => SceneManager.LoadScene(_mainMenuIndex);
}
