using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SfpsEnemyNavScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public SimplePlayerData playerData;

    void Update()
    {
        agent.SetDestination(playerData.playerPos);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }

    // Restart NavMeshAgent after separating from player
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            agent.isStopped = false;
        }
    }
}
