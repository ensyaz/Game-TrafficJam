using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollisionController : MonoBehaviour
{
    public delegate void OnCollect();
    public static event OnCollect OnCollectEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.sharedInstance.GoldCount += 1;
            OnCollectEvent?.Invoke();
            SoundManager.sharedInstance.PlaySound(SoundManager.Sound.GoldCollect);
            Destroy(gameObject);
        }
            
    }
}
