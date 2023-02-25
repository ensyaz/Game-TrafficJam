using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{

    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(2f);
    }

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
            //StartCoroutine(DisapperCollectable(other.gameObject));
            GameManager.sharedInstance.GoldCount += 1;
            EventManager.onCollectAction?.Invoke();
            EventManager.onCollectActionGameobject?.Invoke(other.gameObject);
            SoundManager.sharedInstance.PlaySound(SoundManager.Sound.GoldCollect);
        }

        if (other.CompareTag("Section"))
        {
            EventManager.onCollisionSection?.Invoke(other.gameObject);
        }
    }
}
