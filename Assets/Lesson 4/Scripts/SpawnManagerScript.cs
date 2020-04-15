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

    public int maximum;

    void Start()
    {
        StartCoroutine(SpawnWave(100, 1f));
    }

    IEnumerator SpawnWave(int n, float interval)
    {
        int total = 0;

        while (total < n) {
            foreach (Transform spawner in spawners) {
                yield return new WaitForSeconds(interval);
                if (Vector3.Distance(spawner.position, playerData.playerPos) < minDistance) continue;
                if (total > maximum) continue;

                int randomIndex = Random.Range(0, zombiePrefabs.Count);
                
                GameObject zombie = Instantiate(zombiePrefabs[randomIndex], spawner.position, Quaternion.identity);
                
                total++;
                if (total >= n) break;
            }
        }
    }
}
