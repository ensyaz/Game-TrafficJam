using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    [SerializeField]
    private TextMeshProUGUI goldCountText;
    [SerializeField]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject gameOnUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject backgroundBlur;

    private int _finalScore = 0;
    private int _hold = 0;
    private float _timer = 0;
    private float _interval = 10000f;
    

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

    private void SetGoldAmount()
    {
        goldCountText.text = GameManager.sharedInstance.GoldCount.ToString();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetDistanceText()
    {
        distanceText.text = ((int)playerTransform.position.z).ToString();
    }

    private void ShowScore()
    {
        gameOnUI.SetActive(false);
        gameOverUI.SetActive(true);
        backgroundBlur.SetActive(true);
        CalculateScore();
        StartCoroutine(FinalScore());
    }

    private void CalculateScore()
    {
        _finalScore = (int)playerTransform.position.z + GameManager.sharedInstance.GoldCount;
    }

    private IEnumerator FinalScore()
    {
        if (_hold != _finalScore + 1)
        {
            finalScoreText.text = _hold.ToString();
            _hold += 1;
            yield return null;
        }
    }









}
