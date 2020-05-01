using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfpsShootScript : MonoBehaviour
{
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }
    void Fire()
    {
        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;

        // Check if Zombie is hit
        if (hit.collider.gameObject.tag == "Zombie") {
            SfpsHealthBase zombie = hit.collider.gameObject.GetComponent<SfpsHealthBase>();
            zombie.TakeDamage(1);
        }
    }
}
