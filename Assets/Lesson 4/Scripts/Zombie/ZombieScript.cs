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

    public AudioSource zombieAudio;

    private int health;

    // Attack speed implementation
    private float t = 0;
    private float attackInterval;
    
    void Start()
    {
        SetupNavMeshAgent();

        health = zombieData.initHealth;
        
        attackInterval = 1/zombieData.attackSpd;

        StartCoroutine(MakeNoise());
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

    IEnumerator MakeNoise() {
        while (health > 0) {
            yield return new WaitForSeconds(Random.Range(0.0f, 2.0f));

            zombieAudio.PlayOneShot(zombieData.idleClip);

            yield return new WaitForSeconds(zombieData.idleInterval + Random.Range(0.0f, 2.0f));
        }
    }

    public void TakeDamage(int dmg, Vector3 force, Vector3 point)
    {
        zombieAudio.Stop();
        zombieAudio.PlayOneShot(zombieData.hurtClip);

        health -= dmg;
        if (health > 0) return;

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
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            navMeshAgent.isStopped = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!CanAttack()) return;
        
        if (collider.gameObject.tag == "Player" && navMeshAgent.enabled == true) {
            
            FpsPlayerScript player = collider.gameObject.GetComponent<FpsPlayerScript>();

            zombieAudio.PlayOneShot(zombieData.attackClip);

            player.TakeDamage(zombieData.damage);
        }
    }
}
