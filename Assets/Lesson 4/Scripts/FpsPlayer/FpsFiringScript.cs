using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsFiringScript : MonoBehaviour
{
    public List<FpsWeapon> weapons;
    public Transform canvasTrans;
    public Transform weaponHudTrans;
    public EquippedWeaponData eqWeaponData;

    public PlayerData playerData;
    private FpsWeapon currentWeapon;
    private GameObject currentWeaponModel;
    private int weaponIndex = 0;

    void Start()
    {
        playerData.killCount = 0;
        SwitchWeapon(weapons[weaponIndex]);
    }
    void Update()
    {
        if (playerData.Dead()) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Index cycles from 0 to weapons.Count then repeats
            weaponIndex = (weaponIndex + 1) % weapons.Count;

            // Destroy FpsWeapon GameObject from HUD position
            Destroy(currentWeaponModel);
            SwitchWeapon(weapons[weaponIndex]);
        } 

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }


        if (Input.GetButtonDown("Fire1") && currentWeapon.CanFire()) {
            currentWeapon.Fire();
        }
    }

    void SwitchWeapon(FpsWeapon weapon)
    {
        if (currentWeapon != null) currentWeapon.Dismantle();

        // Set current FpsWeapon
        currentWeapon = weapon;
        eqWeaponData.weapon = currentWeapon;

        // Convert current FpsWeapon's HUD position to local HUD position (bottom right of screen) of weaponHudTrans
        weaponHudTrans.localPosition = currentWeapon.weaponHudPos;

        // Create FpsWeapon GameObject at HUD position
        currentWeaponModel = Instantiate(currentWeapon.weaponPrefab, weaponHudTrans.position, 
        currentWeapon.weaponHudRot.rotation, canvasTrans);

        // Give FpsWeapon GameObject back to script for reference and perform setup
        currentWeapon.Setup(currentWeaponModel);
    }
}
