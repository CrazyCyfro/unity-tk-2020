using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHudScript : MonoBehaviour
{
    private IReloadHUD reloadHud;
    public ActiveWeaponData weaponData;

    void Awake()
    {
        reloadHud = GetComponent<IReloadHUD>();
    }

    void OnDisable()
    {
        ClearData();
        FpsEvents.UpdateWeaponData.RemoveListener(UpdateData);
    }

    void OnEnable()
    {
        FpsEvents.UpdateWeaponData.AddListener(UpdateData);
    }

    void UpdateData()
    {
        weaponData.ammoString = reloadHud.AmmoString();
        weaponData.outOfAmmo = reloadHud.OutOfAmmo();
        weaponData.reloading = reloadHud.Reloading();
    }

    void ClearData()
    {
        weaponData.ammoString = string.Empty;
        weaponData.outOfAmmo = false;
        weaponData.reloading = false;
    }
}
