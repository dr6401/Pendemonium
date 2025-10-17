using UnityEngine;
using System.Collections.Generic;
using System;
public class AnimalSpawnManager : MonoBehaviour
{
    private int aliveSpawners;
    List<GameObject> spawners = new List<GameObject>();
    private bool actionSent = false;

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
        //spawnerText.text = aliveSpawners + "/" + maxSpawners;

        if (aliveSpawners <= 0 && !actionSent)
        {
                AllSpawnerDead?.Invoke();
                actionSent = true;
        }
    }

    public void DecrementAliveSpawnersCounter()
    {
        aliveSpawners--;
        Debug.Log("DecrementAliveSpawnersCounter called, aliveSpawners should be reduced");
    }
}
