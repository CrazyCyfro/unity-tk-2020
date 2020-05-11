using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsWeaponSwitchScript : MonoBehaviour
{
    public int capacity;
    public Transform weaponPos;
    public float thrust;
    private FpsFireManagerScript fireManager;

    void Awake()
    {
        if (capacity <= 0) Debug.LogError("Capacity cannot be zero/negative!");
        if (weaponPos.childCount > capacity) Debug.LogError("Weapons exceed capacity!");

        fireManager = GetComponent<FpsFireManagerScript>();

        if (weaponPos.childCount > 0) AssignActiveWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Switch();
        }
    }

    public void AssignActiveWeapon()
    {
        for (int i = 0; i < weaponPos.childCount; i++) {
            GameObject weapon = weaponPos.GetChild(i).gameObject;
            if (i == 0) {
                weapon.SetActive(true);
            } else if (weaponPos.childCount > capacity && i == 1) {
                weapon.GetComponent<WeaponPickupScript>().Drop(thrust);
            }
        }

        fireManager.AssignHeldWeapon();
    }

    void Switch()
    {
        if (weaponPos.childCount <= 1) return;

        Transform active = weaponPos.GetChild(0);
        active.gameObject.SetActive(false);
        active.SetAsLastSibling();

        AssignActiveWeapon();
    }
}
