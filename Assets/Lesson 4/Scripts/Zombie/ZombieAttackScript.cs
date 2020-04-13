using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackScript : MonoBehaviour
{
    public ZombieData zombieData;
    public NavMeshAgent navMeshAgent;
    public ZombieAudioScript zombieAudio;

    // Attack speed implementation
    private float t = 0;
    private float attackInterval;
    
    void Start()
    {
        attackInterval = 1/zombieData.attackSpd;
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

    void OnTriggerStay(Collider collider)
    {
        // Continue if CanAttack is true
        if (!CanAttack()) return;
        
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            
            FpsHealthScript player = collider.gameObject.GetComponent<FpsHealthScript>();

            zombieAudio.PlayAttackClip();

            player.TakeDamage(zombieData.damage);
        }
    }
}
