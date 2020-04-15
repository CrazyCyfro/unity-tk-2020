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
    public float fireInterval;
    public float throwForce;
    public int damage;

    public float damageRadius;
    
    public float fuse;
    private float t;
    [Header("Explosion settings")]
    public float explosionForce;
    public float explosionRadius;
    public float explosionLift;

    [Header("Prefabs")]
    public GameObject liveGrenadePrefab;
    public GameObject explosionPrefab;
    public override void Setup(GameObject obj)
    {
        weaponObject = obj;
        
        // Set Rigidbody of HUD grenade to not interact with physics
        weaponObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }
    public override bool CanFire()
    {
        if (ammo <= 0) return false;

        if (Time.time - t > fireInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }
    public override void Fire() {
        weaponAudio.PlayOneShot(firingAudioClip);
        
        GameObject liveGrenade = Instantiate(liveGrenadePrefab, grenadeSpawnTrans.position, Quaternion.identity);
        Rigidbody rb = liveGrenade.GetComponent<Rigidbody>();
        rb.AddForce(throwForce * grenadeSpawnTrans.transform.forward);

        StartCoroutine(FuseExplode(liveGrenade));
    }

    IEnumerator FuseExplode(GameObject grenade)
    {

        yield return new WaitForSeconds(fuse);
        AudioSource.PlayClipAtPoint(explosionClip, grenade.transform.position);
        
        Collider[] hitColliders = Physics.OverlapSphere(grenade.transform.position, damageRadius);
        foreach (Collider c in hitColliders) {
            c.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            
            ZombieHealthScript zombie = c.GetComponent<ZombieHealthScript>();
            if (zombie == null) continue;
            zombie.TakeExplosiveForce(explosionForce, grenade.transform.position, explosionRadius, explosionLift);
        }
        
        GameObject explosion = Instantiate(explosionPrefab, grenade.transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        
        Destroy(grenade);
        yield break;
    }
}
