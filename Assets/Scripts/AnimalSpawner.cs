using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject animalPrefab;
    public int baseNumberOfAnimalsToSpawnEachRound;
    public float previouslyAliveAnimalsToSpawnMultiplier = 0.1f;
    private int numberOfAnimalsToSpawn = 10;
    public float spawnRadius = 20f;
    public float spawnHeight = 1f;
    private int maxSpawnAttempts = 10;

    [Header("Clearance Settings")]
    public float clearanceRadius = 1f;
    public Collider nonSpawningZoneCollider;
    public Transform animalsParentFolder;
    private int numberOfAnimalSpawners;
    private Transform animalsFoldersFolder;
    void Start()
    {
        animalsFoldersFolder = transform.parent;
        numberOfAnimalSpawners = animalsFoldersFolder.childCount;
        numberOfAnimalSpawners--; // do not count the FoldersFolder
        int numberOfPreviouslyAliveAnimals = GameManager.Instance.currentAliveAnimals;
        numberOfAnimalsToSpawn = numberOfPreviouslyAliveAnimals;
        numberOfAnimalsToSpawn += (int) (numberOfAnimalsToSpawn * previouslyAliveAnimalsToSpawnMultiplier);
        numberOfAnimalsToSpawn += baseNumberOfAnimalsToSpawnEachRound;
        numberOfAnimalsToSpawn /= numberOfAnimalSpawners;
        SpawnAnimals(numberOfAnimalsToSpawn);
        Debug.Log($"numberOfAnimalSpawners: {numberOfAnimalSpawners} \n numberOfPreviouslyAliveAnimals: {numberOfPreviouslyAliveAnimals} \n baseNumberOfAnimalsToSpawnEachRound: {baseNumberOfAnimalsToSpawnEachRound} \n numberOfAnimalsToSpawn: {numberOfAnimalsToSpawn}");
    }

    void SpawnAnimals(int animalsToSpawn)
    {
        for (int i = 0; i < animalsToSpawn; i++)
        {
            bool foundValidSpot = false;
            Vector3 finalSpawnPosition = Vector3.zero;

            for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
            {
                // Pick random point around spawner
                Vector3 randomOffset = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    spawnHeight,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                Vector3 spawnPosition = transform.position + randomOffset;

                // Check if area is clear
                bool isClear = !Physics.CheckSphere(spawnPosition, clearanceRadius);

                // Check if inside no-spawn zone
                if (nonSpawningZoneCollider != null && 
                    nonSpawningZoneCollider.bounds.Contains(spawnPosition))
                {
                    isClear = false;
                }

                if (isClear)
                {
                    finalSpawnPosition = spawnPosition;
                    foundValidSpot = true;
                    break;
                }
            }

            if (foundValidSpot)
            {
                GameObject newAnimal = Instantiate(animalPrefab, finalSpawnPosition, Quaternion.identity);
                if (animalsParentFolder != null)
                    newAnimal.transform.SetParent(animalsParentFolder);
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSpawnAnimals += SpawnAnimals;
    }
    
    private void OnDisable()
    {
        GameEvents.OnSpawnAnimals -= SpawnAnimals;
    }

    // Optional: draw spawn area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, clearanceRadius);
    }
}
