using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    private int _hold = 1;

    private void Awake()
    {
        StartCoroutine(FinalScore());
    }

    private IEnumerator FinalScore()
    {
        while (_hold != GameManager.sharedInstance.finalScore + 1)
        {
            finalScoreText.text = _hold.ToString();
            _hold += 1;
            yield return null;
        }
    }

    public void ReloadButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


}
