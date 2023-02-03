using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

}
