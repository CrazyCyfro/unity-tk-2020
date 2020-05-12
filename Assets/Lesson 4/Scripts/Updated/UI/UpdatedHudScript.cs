using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatedHudScript : MonoBehaviour
{
    // public Canvas hudCanvas;
    public ActiveWeaponData weaponData;
    public GameObject crosshair;
    public TextMeshProUGUI weaponStatus;
    public TextMeshProUGUI ammoDisplay;

    void Start()
    {
        FpsEvents.UpdateHudEvent.AddListener(UpdateHud);
        weaponStatus.text = string.Empty;
        ammoDisplay.text = string.Empty;
    }

    void UpdateHud()
    {
        ammoDisplay.text = weaponData.ammoString;

        if (weaponData.outOfAmmo) {
            crosshair.SetActive(false);
            weaponStatus.text = "OUT OF AMMO";
        } else if (weaponData.reloading) {
            crosshair.SetActive(false);
            weaponStatus.text = "RELOADING";
        } else {
            crosshair.SetActive(true);
            weaponStatus.text = string.Empty;
        }
    }
}
