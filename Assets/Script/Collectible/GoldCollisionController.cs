using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollisionController : MonoBehaviour
{
    public delegate void OnCollect();
    public event OnCollect OnCollectEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollectEvent?.Invoke();
            Destroy(gameObject);
        }
            
    }
}
