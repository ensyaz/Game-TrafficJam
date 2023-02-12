using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldParticleEffectController : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem collectParticleEffect;

    private Transform _transform;

    private void Awake()
    {
        collectParticleEffect.Stop();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void PlayParticle()
    {
        Debug.Log("test");
        //Instantiate(collectParticleEffect, _transform.position, Quaternion.identity);
        collectParticleEffect.Play();
    }

    
}
