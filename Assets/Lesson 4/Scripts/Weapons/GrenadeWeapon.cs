using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeWeapon : FpsWeapon
{
    public Transform grenadeSpawnTrans;
    [Header("Custom settings start here")]
    public AudioSource weaponAudio;
    public AudioClip explosionClip;

    [Header("Firing settings")]
    public float throwForce;
    public int damage;

    public float damageRadius;
    
    public float fuse;
    [Header("Explosion settings")]
    public float explosionForce;
    public float explosionRadius;
    public float explosionLift;

    [Header("Prefabs")]
    public GameObject liveGrenadePrefab;
    public GameObject explosionPrefab;
    public override void Setup(GameObject obj)
    {
        base.Setup(obj);
        
        // Set Rigidbody of HUD grenade to not interact with physics
        weaponObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public override void Fire() {
        ammo--;

        // Play throwing sound
        weaponAudio.PlayOneShot(firingAudioClip);

        // Store GameObject of thrown grenade
        GameObject liveGrenade = Instantiate(liveGrenadePrefab, grenadeSpawnTrans.position, Quaternion.identity);
        Rigidbody rb = liveGrenade.GetComponent<Rigidbody>();

        // Impart throwing force to grenade
        rb.AddForce(throwForce * grenadeSpawnTrans.transform.forward);

        // Explosion coroutine
        StartCoroutine(FuseExplode(liveGrenade));
    }

    IEnumerator FuseExplode(GameObject grenade)
    {

        yield return new WaitForSeconds(fuse);

        // Play explosion sound at grenade's location with separate AudioSource
        AudioSource.PlayClipAtPoint(explosionClip, grenade.transform.position);
        
        // Check all colliders inside damage radius
        Collider[] hitColliders = Physics.OverlapSphere(grenade.transform.position, damageRadius);
        foreach (Collider c in hitColliders) {
            // Call TakeDamage in all MonoBehaviours that have it
            c.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            
            // Check if we hit a zombie
            ZombieHealthScript zombie = c.GetComponent<ZombieHealthScript>();
            if (zombie == null) continue;

            zombie.TakeExplosiveForce(explosionForce, grenade.transform.position, explosionRadius, explosionLift);
        }
        
        // Create explosion particles and destroy afterwards
        GameObject explosion = Instantiate(explosionPrefab, grenade.transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        
        // Destroy the thrown grenade
        Destroy(grenade);
        yield break;
    }
}
