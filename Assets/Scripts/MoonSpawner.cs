using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSpawner : MonoBehaviour
{
    public GameObject moonPrefab;  // Assign the Moon prefab in Inspector
    [SerializeField] public int moonCount = 10;     // Number of moons to spawn
        [SerializeField] private float spawnInterval = 2f; //time interval per moon spawn
    [SerializeField] public Vector3 spawnArea = new Vector3(10f, 10f, 10f); // X, Y, Z boundaries

    

    public void SpawnMoons()
    {
        StartCoroutine(SpawnMoonsCoroutine());
    }

    private IEnumerator SpawnMoonsCoroutine()
    {
        for (int i = 0; i < moonCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x), 
                Random.Range(-spawnArea.y, spawnArea.y), 
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            GameObject newMoon = Instantiate(moonPrefab, randomPosition, Quaternion.identity);
        
            Debug.Log($"Spawned moon at {randomPosition}");

            if (newMoon == null)
            {
                Debug.LogError("Moon instantiation failed!");
            }

            yield return new WaitForSeconds(spawnInterval); //Wait before spawning another moon
        }
    }
}