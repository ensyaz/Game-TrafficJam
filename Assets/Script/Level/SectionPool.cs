using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] sectionPool;

    private int _currentSectionIndex = 1;
    private int _sectionLocationIndex = 1;
    private int _poolRange = 4;
    private int _sectionLenght = 108;

    private void OnEnable()
    {
        EventManager.onCollisionSection += DisableSection;
        EventManager.onCollisionSection += EnableSection;
    }

    private void OnDisable()
    {
        EventManager.onCollisionSection -= DisableSection;
        EventManager.onCollisionSection -= EnableSection;
    }
    // Enable the next section
    private void EnableSection(GameObject obj)
    {
        _currentSectionIndex += 1;
        _sectionLocationIndex += 1;

        if (_currentSectionIndex == _poolRange)
            _currentSectionIndex = 0;

        sectionPool[_currentSectionIndex].transform.position = new Vector3(0, 0, _sectionLenght * _sectionLocationIndex);
        sectionPool[_currentSectionIndex].SetActive(true);
    }
    // Disable the previous section
    private void DisableSection(GameObject obj)
    {
        foreach (GameObject section in sectionPool)
        {
            if (section == obj)
            {
                section.SetActive(false);
            }
        }
    }

    

 



}
