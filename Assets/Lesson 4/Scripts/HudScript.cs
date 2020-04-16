using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudScript : MonoBehaviour
{
    public EquippedWeaponData eqWeaponData;
    public PlayerData playerData;
    public Canvas canvas;
    public GameObject crosshair;
    public TextMeshProUGUI ammoInfo;
    public TextMeshProUGUI ammoDisplay;
    public TextMeshProUGUI healthDisplay;

    private string noAmmoString = "OUT OF AMMO";
    private string reloadingString = "RELOADING";

    void Update()
    {
        if (playerData.Dead()) {
            canvas.enabled = false;
            return;
        }

        if (eqWeaponData.weapon.HasAmmo()) {
            crosshair.SetActive(true);
            ammoInfo.text = string.Empty;
            ammoDisplay.text = eqWeaponData.weapon.AmmoString();
        } else {
            crosshair.SetActive(false);
            ammoInfo.text = noAmmoString;
            ammoDisplay.text = string.Empty;
        }

        healthDisplay.text = playerData.currentHealth.ToString();

        if (eqWeaponData.weapon.IsReloading()) {
            crosshair.SetActive(false);
            ammoInfo.text = reloadingString;
        }
    }  
}
