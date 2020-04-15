using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : FpsWeapon
{
    private Transform barrelLocation;
    private ParticleSystem muzzleFlashSystem;

    [Header("Custom settings start here")]
    public Camera playerCamera;
    public AudioSource weaponAudio;

    [Header("Firing settings")]
    public float fireRate;
    public float bulletForceMultiplier;
    public int bulletDamage;

    public int magazineSize;
    public float reloadTime;
    private int magazine;
    private int reserve;
    private bool reloading;
    
    [Header("Prefabs")]
    public GameObject bulletImpactPrefab;
    public GameObject muzzleFlashPrefab;

    void Start()
    {
        // Distribute ammo between magazine and reserve
        reserve = ammo;
        magazine = 0;
        reloading = false;
        
        // Quick reload without delay
        int deficit = magazineSize - magazine;   
    
        if (reserve >= deficit) {
            reserve -= deficit;
            magazine = magazineSize;
        } else {
            magazine += reserve;
            reserve = 0;
        }
    }

    void Update()
    {
        // Update ammo and force reload every frame
        ammo = reserve + magazine;
        if (magazine <= 0) Reload();
    }

    public override void Setup(GameObject obj)
    {
        base.Setup(obj);

        // Set reloading to false so it does not get stuck at true if weapon switched during reload previously
        reloading = false;
        
        // Calculate fireInterval
        fireInterval = 1/fireRate;

        // Store BarrelLocation transform
        barrelLocation = weaponObject.transform.Find("BarrelLocation");
        
        // Create muzzle flash ParticleSystem at BarrelLocation and store it
        muzzleFlashSystem = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation, weaponObject.transform)
        .GetComponent<ParticleSystem>();
    }

    public override void Dismantle()
    {
        StopAllCoroutines();
    }
    public override void Fire()
    {
        magazine--;

        // Play gunshot sound
        weaponAudio.PlayOneShot(firingAudioClip);

        // Play muzzle flash particles
        muzzleFlashSystem.Play();

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;

        // Spawn impact particles, destroy after animation is over
        GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);

        // Check if a zombie was hit
        if (hit.collider.gameObject.tag == "Zombie") {
            ZombieHealthScript zombie = hit.collider.gameObject.GetComponent<ZombieHealthScript>();

            // Zombie takes numerical damage
            zombie.TakeDamage(bulletDamage);

            // Type of force to inflict on zombie ragdoll (on killing blow)
            zombie.TakeBulletForce(-hit.normal * bulletForceMultiplier, hit.point);
        }
    }
    
    public override void Reload()
    {
        // Do not proceed if still reloading
        if (reloading) return;

        // magazine is full
        if (magazine == magazineSize) return;

        // or reserve is empty
        if (reserve <= 0) return; 
        
        StartCoroutine(ReloadTimer());   
    }

    IEnumerator ReloadTimer()
    {
        // Set reloading to true and wait
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        
        // Reloading code
        int deficit = magazineSize - magazine;   
    
        if (reserve >= deficit) {
            reserve -= deficit;
            magazine = magazineSize;
        } else {
            magazine += reserve;
            reserve = 0;
        }

        reloading = false;
        
        
        yield break;
    }

    public override bool IsReloading()
    {
        return reloading;
    }

    public override string AmmoString()
    {
        return magazine.ToString() + "/" + reserve.ToString();
    }

}
