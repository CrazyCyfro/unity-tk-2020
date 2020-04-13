using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsShootScript : MonoBehaviour
{
    public AudioSource gunAudio;
    public Camera playerCamera;
    
    [Header("Firing settings")]
    public float fireRate;
    public float bulletForceMultiplier;
    public int bulletDamage;
    private float t = 0;
    private float fireInterval;
    [Header("Bullet impact settings")]
    public GameObject bulletImpactPrefab;
    
    void Start()
    {
        // Calculate fireInterval from fireRate
        fireInterval = 1/fireRate;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && CanShoot()) {
            Shoot();
        }
    }

    void Shoot()
    {
        gunAudio.Play();

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;

        // Spawn impact particles, destroy after animation is over
        GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);

        // Check if a zombie was hit
        if (hit.collider.gameObject.tag == "Zombie") {
            ZombieHealthScript zombie = hit.collider.gameObject.GetComponent<ZombieHealthScript>();
            zombie.TakeBulletDamage(bulletDamage, -hit.normal * bulletForceMultiplier, hit.point);
        }
    }

    // Returns true after time passed is more than fireInterval
    bool CanShoot()
    {
        if (Time.time - t > fireInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }
}
