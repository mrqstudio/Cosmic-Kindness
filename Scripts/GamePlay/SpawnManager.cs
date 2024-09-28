using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform[] spawnPoints; // Array of positions to spawn objects

    private GameObject currentObject; // The currently spawned object

    // Start is called before the first frame update
    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true) // Loop indefinitely
        {
            // Randomly select an object and a spawn point
            int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

            // Spawn the object at the selected spawn point
            currentObject = Instantiate(objectsToSpawn[randomObjectIndex], spawnPoints[randomSpawnIndex].position, spawnPoints[randomSpawnIndex].rotation);

            // Wait until the current object is destroyed
            while (currentObject != null)
            {
                yield return null; // Wait for the next frame
            }

            // Optionally wait before spawning the next object (e.g., 1 second)
            yield return new WaitForSeconds(1f);
        }
    }
}
