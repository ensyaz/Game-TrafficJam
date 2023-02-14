using System;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem goldCollectEffect;

    private Transform _goldCollectEffectTransform;

    private void Awake()
    {
        _goldCollectEffectTransform = goldCollectEffect.transform;
    }

    private void OnEnable()
    {
        EventManager.onCollectActionGameobject += PlayGoldEffect;
    }

    private void OnDisable()
    {
        EventManager.onCollectActionGameobject -= PlayGoldEffect;
    }

    private void PlayGoldEffect(GameObject gameObject)
    {
        _goldCollectEffectTransform.position = gameObject.transform.position;
        goldCollectEffect.Play();
    }



    






}
