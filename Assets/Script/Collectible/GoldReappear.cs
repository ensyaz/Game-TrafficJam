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
        EventManager.onCollectActionGameobject += DisappearGolds;
    }

    private void OnDisable()
    {
        EventManager.onCollectActionGameobject -= DisappearGolds;
    }
    // Disappear the golds for 2 seconds when they get collected
    private void DisappearGolds(GameObject obj)
    {
        StartCoroutine(DisappearCollectable(obj));
    }

    private IEnumerator DisappearCollectable(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().enabled = false;

        yield return _delay;

        if (!GameManager.sharedInstance.IsGameOver)
            obj.GetComponent<MeshRenderer>().enabled = true;
    }


}
