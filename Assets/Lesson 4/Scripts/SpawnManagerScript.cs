using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public List<Transform> spawners;
    public GameObject regZombiePrefab;

    void Start()
    {
        StartCoroutine(SpawnWave(10, 2.0f));
    }

    IEnumerator SpawnWave(int n, float interval)
    {
        int total = 0;

        while (total < n) {
            foreach (Transform spawner in spawners) {
                Instantiate(regZombiePrefab, spawner.position, Quaternion.identity);
                total++;
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
