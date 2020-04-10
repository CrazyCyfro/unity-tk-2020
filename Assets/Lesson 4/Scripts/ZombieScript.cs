using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public PlayerData playerData;
    public ZombieData zombieData;

    // Attack speed implementation
    private float t = 0;
    private float period;
    
    void Start()
    {
        SetupNavMeshAgent();
        
        period = 1/zombieData.attackSpd;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(playerData.playerPos);
    }

    bool CanAttack()
    {
        float cooldown = Time.time - t;

        if (cooldown > period) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    void SetupNavMeshAgent()
    {
        navMeshAgent.speed = zombieData.speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            navMeshAgent.speed = 0;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            navMeshAgent.speed = zombieData.speed;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!CanAttack()) return;
        
        if (collider.gameObject.tag == "Player") {
            
            FpsPlayerScript player = collider.gameObject.GetComponent<FpsPlayerScript>();
            
            if (player == null) {
                Debug.Log("FpsPlayerScript not found!");
                return;
            }

            player.TakeDamage(zombieData.damage);
        }
    }
}
