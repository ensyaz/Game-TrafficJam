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
            SoundManager.sharedInstance.playerCollisionAudioSource.Play();
        }    
    }
    // Whether player is on ground or not
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            GameManager.sharedInstance.IsGrounded = true;
    }
    // Whether player is on ground or not
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            GameManager.sharedInstance.IsGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player collide with gold
        if (other.CompareTag("Collectable"))
        {
            GameManager.sharedInstance.GoldCount += 1;
            EventManager.onCollectAction?.Invoke();
            EventManager.onCollectActionGameobject?.Invoke(other.gameObject);
            SoundManager.sharedInstance.goldAudioSource.Play();
        }
        // When player collide with section collider in order to disable previous section
        if (other.CompareTag("Section"))
        {
            EventManager.onCollisionSection?.Invoke(other.gameObject);
        }
    }
}
