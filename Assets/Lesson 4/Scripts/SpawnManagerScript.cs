using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManagerScript : MonoBehaviour
{
    public PlayerData playerData;
    public SpawnData spawnData;
    public List<Transform> spawners;
    public List<GameObject> zombiePrefabs;
    public GameObject resupplyPrefab;

    public float minDistance;
    public int maxZombies;

    void Start()
    {
        spawnData.zombies = 0;
        spawnData.resupply = 0;
        StartCoroutine(SpawnWave(2f));
    }

    IEnumerator SpawnWave(float interval)
    {

        while (true) {
            yield return new WaitForSeconds(interval);

            // Proceed only if player distance to spawner is greater than minDistance
            Transform spawner = spawners[Random.Range(0, spawners.Count)];
            if (Vector3.Distance(spawner.position, playerData.playerPos) < minDistance) continue;

            // Spawn an ammo resupply if there are none at 20% chance per cycle
            if (spawnData.resupply <= 0 && Random.value > 0.8) {
                Instantiate(resupplyPrefab, spawner.position, Quaternion.identity);
                spawnData.resupply++;
            }

            // Proceed only if number of zombies alive is less than maximum allowed
            if (spawnData.zombies >= maxZombies) continue;

            // Spawn random zombie from zombiePrefabs
            int randomIndex = Random.Range(0, zombiePrefabs.Count);
            GameObject zombie = Instantiate(zombiePrefabs[randomIndex], spawner.position, Quaternion.identity);
            spawnData.zombies++;

            
            
        }
    }
}
