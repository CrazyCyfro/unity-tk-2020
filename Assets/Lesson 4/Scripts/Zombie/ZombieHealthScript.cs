using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealthScript : MonoBehaviour
{
    public ZombieData zombieData;
    public ZombieAudioScript zombieAudio;
    public NavMeshAgent navMeshAgent;

    public Rigidbody rb;
    public Collider col;
    private int health;
    
    void Start()
    {
        health = zombieData.initHealth;

        StartCoroutine(PlayIdleAudio());
    }

    IEnumerator PlayIdleAudio()
    {

        // Play idle clip while zombie is alive
        while (health > 0) {

            zombieAudio.PlayIdleClip();
            yield return new WaitForSeconds(zombieData.idleInterval);
        }

        // Stop coroutine
        yield break;
    }

    public void TakeBulletDamage(int dmg, Vector3 force, Vector3 point)
    {
        zombieAudio.PlayHurtClip();

        health -= dmg;
        if (health > 0) return;

        // If health < 0, disable NavMeshAgent and enable ragdoll physics
        navMeshAgent.enabled = false;
        col.isTrigger = false;
        rb.isKinematic = false;
        
        // Impart force from bullet
        rb.AddForceAtPosition(force, point);

        // Destroy zombie after delay
        Destroy(gameObject, zombieData.destroyDelay);
    }
}
