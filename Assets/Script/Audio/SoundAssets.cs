using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets sharedInstance;
    public SoundAudioClip[] soundAudioClipArray;

    private void Awake()
    {
        sharedInstance = this;
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
