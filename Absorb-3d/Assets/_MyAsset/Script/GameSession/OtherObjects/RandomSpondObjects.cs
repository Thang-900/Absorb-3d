using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    public int currentLevel = 1;
    public GameObject[] objectsToSpawn;
    public float spawnZone = 10f;
    public float gapDistance = 2f;
    public int maxObjects = 10;

    private List<Vector3> spawnedPositions = new List<Vector3>();
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void OnEnable()
    {
        SpawnGameObjects();
    }

    private void OnDisable()
    {
        ClearOldSpawns();
    }

    private void SpawnGameObjects()
    {
        int countObject = 0;
        int attempts = 0;
        int maxAttempts = maxObjects * 10;

        while (countObject < maxObjects && attempts < maxAttempts)
        {
            attempts++;

            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject objToSpawn = objectsToSpawn[randomIndex];

            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnZone;
            randomPosition.y = 5f;

            bool canTakePosition = true;
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(randomPosition, pos) < gapDistance)
                {
                    canTakePosition = false;
                    break;
                }
            }

            if (canTakePosition)
            {
                GameObject spawned = Instantiate(objToSpawn, randomPosition, Quaternion.identity);
                spawnedObjects.Add(spawned);
                spawnedPositions.Add(randomPosition);
                countObject++;
            }
        }
    }

    private void ClearOldSpawns()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
        spawnedPositions.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnZone);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gapDistance);
    }
}
