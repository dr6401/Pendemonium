using UnityEngine;
using UnityEngine.Serialization;

public class AnimalSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject animalPrefab;
    public int numberOfAnimalsToSpawn = 10;
    public float spawnRadius = 20f;
    public float spawnHeight = 1f;
    public int maxSpawnAttempts = 10;

    [Header("Clearance Settings")]
    public float clearanceRadius = 1f;
    public Collider nonSpawningZoneCollider;
    public Transform animalsParentFolder;

    void Start()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        for (int i = 0; i < numberOfAnimalsToSpawn; i++)
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

                Debug.Log($"Spawned {animalPrefab.name} at {finalSpawnPosition}");
            }
            else
            {
                Debug.LogWarning($"⚠️ Couldn’t find valid spot for animal #{i} in {name}");
            }
        }
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
