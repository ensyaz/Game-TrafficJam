using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject goldImageObject;
    [SerializeField]
    private GameObject goldCountTextObject;
    [SerializeField]
    private GameObject distanceTextObject;
    [SerializeField]
    private GameObject finalScoreTextObject;

    private TextMeshProUGUI _goldCountText;
    private TextMeshProUGUI _distanceText;

    private int _finalScore = 0;



    private float _timer = 0;
    private float _interval = 10000f;
    private int _hold = 0;

    private void Awake()
    {
        _goldCountText = goldCountTextObject.GetComponent<TextMeshProUGUI>();
        _distanceText = distanceTextObject.GetComponent<TextMeshProUGUI>();
    }

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
        _goldCountText.text = GameManager.sharedInstance.GoldCount.ToString();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetDistanceText()
    {
        _distanceText.text = ((int)playerTransform.position.z).ToString();
    }

    private void CalculateScore()
    {
        _finalScore = (int)playerTransform.position.z + GameManager.sharedInstance.GoldCount;
    }

    private void ShowScore()
    {
        goldImageObject.SetActive(false);
        goldCountTextObject.SetActive(false);
        distanceTextObject.SetActive(false);
        finalScoreTextObject.SetActive(true);
        CalculateScore();
        StartCoroutine(FinalScore());
    }

    private IEnumerator FinalScore()
    {
        if (_hold != _finalScore)
        {
            scoreText.text = _hold.ToString();
            _hold += 1;
            yield return null;
        }
    }





}
