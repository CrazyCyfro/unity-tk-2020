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
            yield return new WaitForSeconds(zombieData.idleInterval);
            if (Random.value > 0.95) continue;
            zombieAudio.PlayIdleClip();
        }

        // Stop coroutine
        yield break;
    }

    public void TakeDamage(int dmg)
    {

        if (Random.value > 0.5)
            zombieAudio.PlayHurtClip();

        health -= dmg;
        if (health > 0) return;

        // If health < 0, disable NavMeshAgent and enable ragdoll physics
        navMeshAgent.enabled = false;
        col.isTrigger = false;
        rb.isKinematic = false;

        // Destroy zombie after delay
        Destroy(gameObject, zombieData.destroyDelay);
    }

    public void TakeBulletForce(Vector3 force, Vector3 point)
    {
        if (navMeshAgent.enabled) return;

        // Impart force from bullet
        rb.AddForceAtPosition(force, point);
    }

    public void TakeExplosiveForce(float force, Vector3 centre, float radius, float up)
    {
        if (navMeshAgent.enabled) return;

        // Impart force from explosion
        rb.AddExplosionForce(force, centre, radius, up);
    }
}
