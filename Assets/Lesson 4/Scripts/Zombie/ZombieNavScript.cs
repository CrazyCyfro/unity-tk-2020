using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public PlayerData playerData;
    public ZombieData zombieData;

    void Start()
    {
        SetupNavMeshAgent();
        StartCoroutine(FollowPlayer());
    }

    // Calls SetDestination between every updateInterval seconds
    IEnumerator FollowPlayer()
    {
        while (navMeshAgent.enabled == true) {
            navMeshAgent.SetDestination(playerData.playerPos);
            yield return new WaitForSeconds(zombieData.updateInterval);
        }

        yield break;
    }

    // Initialize settings for NavMeshAgent
    void SetupNavMeshAgent()
    {
        navMeshAgent.speed = zombieData.speed;
    }


    // Stops NavMeshAgent after colliding with player
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    // Restart NavMeshAgent after separating from player
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            navMeshAgent.isStopped = false;
        }
    }
}
