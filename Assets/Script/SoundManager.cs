using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance;

    public enum Sound
    {
        GoldCollect,
        PlayerCollision,
        GameWin
    }

    private AudioSource _audioSource;

    private void Awake()
    {
        sharedInstance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private AudioClip GetAudioClip(Sound sound)
    {
         foreach(SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.sharedInstance.soundAudioClipArray)
         {
             if (soundAudioClip.sound == sound)
             {
                return soundAudioClip.audioClip;
             }
         }

        return null;
    }







}
