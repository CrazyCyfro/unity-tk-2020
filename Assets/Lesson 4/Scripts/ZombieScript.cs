using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public PlayerData playerData;
    public ZombieData zombieData;
    public Rigidbody rb;
    public Collider col;

    // Attack speed implementation
    private float t = 0;
    private float attackInterval;
    
    void Start()
    {
        SetupNavMeshAgent();
        
        attackInterval = 1/zombieData.attackSpd;
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.enabled == true)
            navMeshAgent.SetDestination(playerData.playerPos);
    }

    bool CanAttack()
    {
        if (Time.time - t > attackInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    public void TakeDamage(Vector3 force, Vector3 point)
    {
        navMeshAgent.enabled = false;
        
        col.isTrigger = false;
        rb.isKinematic = false;
        rb.AddForceAtPosition(force, point);

        Destroy(gameObject, zombieData.destroyDelay);
    }

    void SetupNavMeshAgent()
    {
        navMeshAgent.speed = zombieData.speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            navMeshAgent.isStopped = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!CanAttack()) return;
        
        if (collider.gameObject.tag == "Player") {
            
            FpsPlayerScript player = collider.gameObject.GetComponent<FpsPlayerScript>();

            player.TakeDamage(zombieData.damage);
        }
    }
}
