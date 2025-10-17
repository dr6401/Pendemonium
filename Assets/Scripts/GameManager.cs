using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<Transform> animalFolders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AreAllSpawnersExtinct())
        {
            Debug.Log("YOU LOST");
        }
    }

    bool AreAllSpawnersExtinct()
    {
        foreach (var animalFolder in animalFolders)
        {
            if (animalFolder.childCount >= 0)
            {
                return false;
            }
        }
        return true;
    }
}
