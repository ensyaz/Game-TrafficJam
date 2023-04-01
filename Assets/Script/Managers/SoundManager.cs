using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        GoldCollect,
        PlayerCollision,
        Environment,
        Background
    }

    public static SoundManager sharedInstance;

    public AudioSource goldAudioSource;
    public AudioSource trafficSoundAudioSource;
    public AudioSource citySoundAudioSource;
    public AudioSource playerCollisionAudioSource;
    public AudioSource backgroundAudioSource;

    private int goldAudioIndex = 0;
    private int playerCollisionAudioIndex = 1;
    private int trafficAudioIndex = 2;
    private int cityAudioIndex = 3;
    private int backgroundAudioIndex = 4;
    
    private void Awake()
    {
        if (sharedInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitAudioSource();
    }

    private void InitAudioSource()
    {
        ASGold();
        ASPlayerCollision();
        ASTraffic();
        ASCity();
        ASBackground();
    }

    private void ASGold()
    {
        goldAudioSource = gameObject.AddComponent<AudioSource>();
        goldAudioSource.playOnAwake = false;
        goldAudioSource.clip = SoundAssets.sharedInstance.soundAudioClipArray[goldAudioIndex].audioClip;
    }

    private void ASPlayerCollision()
    {
        playerCollisionAudioSource = gameObject.AddComponent<AudioSource>();
        playerCollisionAudioSource.playOnAwake = false;
        playerCollisionAudioSource.clip = SoundAssets.sharedInstance.soundAudioClipArray[playerCollisionAudioIndex].audioClip;
    }
    
    private void ASTraffic()
    {
        trafficSoundAudioSource = gameObject.AddComponent<AudioSource>();
        trafficSoundAudioSource.loop = true;
        trafficSoundAudioSource.clip = SoundAssets.sharedInstance.soundAudioClipArray[trafficAudioIndex].audioClip;
        trafficSoundAudioSource.Play();
    }

    private void ASCity()
    {
        citySoundAudioSource = gameObject.AddComponent<AudioSource>();
        citySoundAudioSource.loop = true;
        citySoundAudioSource.clip = SoundAssets.sharedInstance.soundAudioClipArray[cityAudioIndex].audioClip;
        citySoundAudioSource.Play();
    }

    private void ASBackground()
    {
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundAudioSource.loop = true;
        backgroundAudioSource.clip = SoundAssets.sharedInstance.soundAudioClipArray[backgroundAudioIndex].audioClip;
        backgroundAudioSource.Play();
    }
}
