using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPickupDropScript : MonoBehaviour
{
    public float pickupDistance;
    public float thrust;
    public Transform weaponPos;
    public Camera playerCamera;
    public LayerMask weaponMask;
    private FpsWeaponSwitchScript switcher;

    void Start()
    {
        switcher = GetComponent<FpsWeaponSwitchScript>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (weaponPos.childCount == 0) return;
            WeaponPickupScript weapon = weaponPos.GetComponentInChildren<WeaponPickupScript>();
            weapon.Drop(thrust);
            switcher.AssignActiveWeapon();
        }

        // Continue if weapon exists in pickup range
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, 
        out hit, pickupDistance, weaponMask)) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponPickupScript weapon = hit.collider.GetComponentInParent<WeaponPickupScript>();
            weapon.Pickup(weaponPos);
            switcher.AssignActiveWeapon();
        }
    }
}
