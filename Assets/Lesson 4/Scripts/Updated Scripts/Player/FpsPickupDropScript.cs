using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPickupDropScript : MonoBehaviour
{
    public float pickupDistance;
    public Transform weaponPos;
    public Transform dropOffPos;
    public LayerMask weaponMask;
    private Camera playerCamera;
    private FpsWeaponSwitchScript switcher;

    void Start()
    {
        switcher = GetComponent<FpsWeaponSwitchScript>();
        playerCamera = GetComponentInChildren<Camera>();
    }
    
    void Update()
    {
        // F to drop weapon
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Get WeaponPickupScript from current active weapon
            WeaponPickupScript weapon = weaponPos.GetComponentInChildren<WeaponPickupScript>();
            if (weapon != null) {
                // Drop weapon and assign new active weapon
                weapon.Drop(dropOffPos.position);
                switcher.AssignActiveWeapon();
            }
        }

        // Continue if weapon exists in pickup range
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, 
        out hit, pickupDistance, weaponMask)) return;

        // E to pick up weapon
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get WeaponPickupScript, call Pickup and assign it as active weapon
            WeaponPickupScript weapon = hit.collider.GetComponentInParent<WeaponPickupScript>();
            switcher.AssignActiveWeapon(weapon.Pickup());
        }
    }
}
