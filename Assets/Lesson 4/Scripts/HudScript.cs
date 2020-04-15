using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudScript : MonoBehaviour
{
    public EquippedWeaponData eqWeaponData;
    public GameObject crosshair;
    public TextMeshProUGUI ammoInfo;
    public TextMeshProUGUI ammoDisplay;

    private string noAmmoString = "Out of ammo";
    private string reloadingString = "Reloading";

    void Update()
    {
        if (eqWeaponData.weapon.HasAmmo()) {
            if (!crosshair.activeSelf) crosshair.SetActive(true);
            if (!ammoInfo.text.Equals(string.Empty)) ammoInfo.text = string.Empty;
            ammoDisplay.text = eqWeaponData.weapon.AmmoString();
        } else {
            if (crosshair.activeSelf) crosshair.SetActive(false);
            if (!ammoInfo.text.Equals(noAmmoString)) ammoInfo.text = noAmmoString;
            ammoDisplay.text = string.Empty;
        }

        if (eqWeaponData.weapon.IsReloading()) {
            if (crosshair.activeSelf) crosshair.SetActive(false);
            if (!ammoInfo.text.Equals(reloadingString)) ammoInfo.text = reloadingString;
            return;
        }
    }  
}
