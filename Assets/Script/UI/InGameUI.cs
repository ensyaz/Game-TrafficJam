using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _goldCountText;

    private void OnEnable()
    {
        EventManager.onCollectAction += SetGoldAmount;
    }

    private void OnDisable()
    {
        EventManager.onCollectAction -= SetGoldAmount;
    }

    private void SetGoldAmount()
    {
        _goldCountText.text = GameManager.sharedInstance.GoldCount.ToString();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
