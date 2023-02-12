using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.sharedInstance.IsGameOver = true;
            SoundManager.sharedInstance.PlaySound(SoundManager.Sound.PlayerCollision);
        }
            
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            GameManager.sharedInstance.IsGrounded = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            GameManager.sharedInstance.IsGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.GetComponent<GoldParticleEffectController>().PlayParticle();
            GameManager.sharedInstance.GoldCount += 1;
            EventManager.OnCollectEvent?.Invoke();
            SoundManager.sharedInstance.PlaySound(SoundManager.Sound.GoldCollect);
            Destroy(other.gameObject);
        }

    }
}
