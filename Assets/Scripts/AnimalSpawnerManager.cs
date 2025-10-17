using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
 
public class AnimalSpawnManager : MonoBehaviour
{
    private int aliveSpawners;
    List<GameObject> spawners = new List<GameObject>();
    private bool actionSent = false;
    public int amountOfAnimalsToSpawn = 10;
    public TMP_Text animalsNumberText;
    public Transform animalsFoldersFolder;
    private List<Transform> animalFolders;
    
    
    public static event Action AllSpawnerDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimalSpawner[] animalSpawners = FindObjectsByType<AnimalSpawner>(FindObjectsSortMode.None);
        foreach(AnimalSpawner animalSpawner in animalSpawners)
        {
            spawners.Add(animalSpawner.gameObject);
        }
        aliveSpawners = spawners.Count;
    }

    // Update is called once per frame
    void Update()
    {

        if (aliveSpawners <= 0 && !actionSent)
        {
                AllSpawnerDead?.Invoke();
                actionSent = true;
        }
        animalsNumberText.text = $"{GetTotalAliveAnimals()}";
    }

    public void SpawnAnimals()
    {
        GameEvents.OnSpawnAnimals?.Invoke(amountOfAnimalsToSpawn);
    }

    private int GetTotalAliveAnimals()
    {
        if (animalsFoldersFolder == null) return 0;
        
        int total = 0;
        foreach (Transform animalFolder in animalsFoldersFolder)
        {
            total += animalFolder.childCount;
        }

        return total;
    }
    public void DecrementAliveSpawnersCounter()
    {
        aliveSpawners--;
        Debug.Log("DecrementAliveSpawnersCounter called, aliveSpawners should be reduced");
    }
}
