using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goldCountText;
    [SerializeField]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject settingsUI;

    [SerializeField]
    private Transform playerTransform;

    private void OnEnable()
    {
        EventManager.onCollectAction += SetGoldAmount;
        GameManager.sharedInstance.OnGameOverEvent += ShowScore;
    }

    private void OnDisable()
    {
        EventManager.onCollectAction -= SetGoldAmount;
        GameManager.sharedInstance.OnGameOverEvent -= ShowScore;
    }

    private void Update()
    {
        SetDistanceText();
    }

    private void SetDistanceText() => distanceText.text = ((int)playerTransform.position.z).ToString();
    private void SetGoldAmount() => goldCountText.text = GameManager.sharedInstance.GoldCount.ToString();


    private void ShowScore()
    {
        inGameUI.SetActive(false);
        GameManager.sharedInstance.finalScore = SetFinalScore();
        gameOverUI.SetActive(true);
    }

    private int SetFinalScore() 
    {
        return (int)playerTransform.position.z + GameManager.sharedInstance.GoldCount;
    }

    public void SettingsButton() => settingsUI.SetActive(true);


    

    









}
