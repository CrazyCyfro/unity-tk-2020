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
        StartCoroutine(SpawnWave(100, 1f));
    }

    IEnumerator SpawnWave(int n, float interval)
    {
        while (spawnData.zombies < n) {
            foreach (Transform spawner in spawners) {
                yield return null;
                if (Vector3.Distance(spawner.position, playerData.playerPos) < minDistance) continue;
                if (spawnData.zombies > maxZombies) continue;

                yield return new WaitForSeconds(interval);

                int randomIndex = Random.Range(0, zombiePrefabs.Count);
                
                GameObject zombie = Instantiate(zombiePrefabs[randomIndex], spawner.position, Quaternion.identity);

                spawnData.zombies++;

                if (spawnData.resupply <= 0 && Random.value > 0.8) {
                    Instantiate(resupplyPrefab, spawner.position, Quaternion.identity);
                    spawnData.resupply++;
                } 

                if (spawnData.zombies >= n) break;
            }
        }
        yield break;
    }
}
