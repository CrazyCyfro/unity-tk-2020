using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManagerScript : MonoBehaviour
{
    public PlayerData playerData;
    public float minDistance;
    public List<Transform> spawners;
    public List<GameObject> zombiePrefabs;

    void Start()
    {
        StartCoroutine(SpawnWave(100, 1f));
    }

    IEnumerator SpawnWave(int n, float interval)
    {
        int total = 0;

        while (total < n) {
            foreach (Transform spawner in spawners) {
                if (Vector3.Distance(spawner.position, playerData.playerPos) < minDistance) continue;

                int randomIndex = Random.Range(0, zombiePrefabs.Count);

                Vector3 spawnPos = Vector3.zero;

                // NavMeshHit hit;
                // if (NavMesh.SamplePosition(spawner.position, out hit, 5, NavMesh.AllAreas)) {
                //     spawnPos = hit.position;
                // }
                
                GameObject zombie = Instantiate(zombiePrefabs[randomIndex], spawner.position, Quaternion.identity);
                
                total++;
                if (total >= n) break;

                yield return new WaitForSeconds(interval);
            }
        }
    }
}
