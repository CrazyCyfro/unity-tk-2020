﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : FpsWeapon
{
    private Transform barrelLocation;
    private GameObject gunObject;

    private ParticleSystem muzzleFlashSystem;
    public Transform canvasTrans;

    [Header("Custom settings start here")]
    public Camera playerCamera;
    public AudioSource weaponAudio;

    [Header("Firing settings")]
    public float fireRate;
    public float bulletForceMultiplier;
    public int bulletDamage;
    private float t = 0;
    private float fireInterval;
    [Header("Prefabs")]
    public GameObject bulletImpactPrefab;
    public GameObject muzzleFlashPrefab;

    void Start()
    {
        fireInterval = 1/fireRate;

        muzzleFlashSystem = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation, weaponObject.transform)
        .GetComponent<ParticleSystem>();
    }
    
    public override bool CanFire()
    {
        if (Time.time - t > fireInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }
    public override void Fire()
    {
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
            zombie.TakeDamage(bulletDamage);
            zombie.TakeBulletForce(-hit.normal * bulletForceMultiplier, hit.point);
        }
    }

    public override void Setup(GameObject obj)
    {
        weaponObject = obj;

        barrelLocation = weaponObject.transform.Find("BarrelLocation");
        muzzleFlashSystem = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation, weaponObject.transform)
        .GetComponent<ParticleSystem>();
    }
}
