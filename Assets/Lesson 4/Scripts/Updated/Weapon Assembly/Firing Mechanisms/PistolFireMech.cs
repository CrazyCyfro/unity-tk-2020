using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFireMech : FireMechanism
{
    public GameObject bulletImpactPrefab;
    Camera playerCamera;
    public override void Fire()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;

        // Spawn impact particles, destroy after animation is over
        GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
