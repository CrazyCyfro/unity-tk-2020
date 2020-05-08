using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsFireManagerScript : MonoBehaviour
{
    public Transform weaponPos;
    private FireMechanism fireMech;
    private ReloadSystem reloadSys;

    void Update()
    {
        if (weaponPos.childCount == 0) return;

        if (fireMech.mode == FireMechanism.FireMode.Semi) {
            if (Input.GetButtonDown("Fire1")) {
                FireWeapon();
            }
        } else if (fireMech.mode == FireMechanism.FireMode.Auto) {
            if (Input.GetButton("Fire1")) {
                FireWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            reloadSys.Reload();
        }
    }

    public void AssignHeldWeapon()
    {
        fireMech = weaponPos.GetComponentInChildren<FireMechanism>();
        reloadSys = weaponPos.GetComponentInChildren<ReloadSystem>();
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && reloadSys.CanFire()) {
            fireMech.Fire();
            reloadSys.Fired();
        }
    }
}
