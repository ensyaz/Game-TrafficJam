using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReappear : MonoBehaviour
{
    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(2f);
    }

    private void OnEnable()
    {
        EventManager.onCollectActionGameobject += DisappearRings;
    }

    private void OnDisable()
    {
        EventManager.onCollectActionGameobject -= DisappearRings;
    }

    private void DisappearRings(GameObject obj)
    {
        StartCoroutine(DisappearCollectable(obj));
    }

    IEnumerator DisappearCollectable(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().enabled = false;

        yield return _delay;

        if (!GameManager.sharedInstance.IsGameOver)
            obj.GetComponent<MeshRenderer>().enabled = true;
    }


}