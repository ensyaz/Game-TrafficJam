using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _goldCountText;

    private void OnEnable()
    {
        GoldCollisionController.OnCollectEvent += SetGoldAmount;
    }

    private void OnDisable()
    {
        GoldCollisionController.OnCollectEvent -= SetGoldAmount;
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