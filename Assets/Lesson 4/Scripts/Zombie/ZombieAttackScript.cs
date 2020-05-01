using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackScript : MonoBehaviour
{
    public ZombieData zombieData;
    public NavMeshAgent navMeshAgent;
    public ZombieAudioScript zombieAudio;

    // Attack implementation
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            FpsHealthScript player = collider.gameObject.GetComponent<FpsHealthScript>();
            StartCoroutine(Attacking(player));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            Debug.Log("Stopping attacks");
            StopAllCoroutines();
        }
    }

    IEnumerator Attacking(FpsHealthScript player)
    {
        while (true)
        {
            player.TakeDamage(zombieData.damage);
        
            if (Random.value > 0.5) zombieAudio.PlayAttackClip();

            yield return new WaitForSeconds(attackInterval);
        }
    }
}
