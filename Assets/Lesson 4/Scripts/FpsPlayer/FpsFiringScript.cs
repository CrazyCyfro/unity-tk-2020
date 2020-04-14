using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsFiringScript : MonoBehaviour
{
    public List<FpsWeapon> weapons;
    public Transform canvasTrans;

    public Transform weaponHudTrans;
    private FpsWeapon currentWeapon;
    private GameObject currentWeaponModel;
    private int weaponIndex = 0;

    void Start()
    {
        currentWeapon = weapons[weaponIndex];
        weaponHudTrans.transform.localPosition = currentWeapon.weaponHudLoc;
        currentWeaponModel = Instantiate(currentWeapon.weaponPrefab, weaponHudTrans.position, 
        currentWeapon.weaponHudRot.rotation, canvasTrans);
        currentWeapon.Setup(currentWeaponModel);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponIndex = (weaponIndex + 1) % weapons.Count;
            SwitchWeapon(weapons[weaponIndex]);
        } 

        if (Input.GetButtonDown("Fire1") && currentWeapon.CanFire()) {
            currentWeapon.Fire();
        }
    }

    void SwitchWeapon(FpsWeapon weapon)
    {
        Destroy(currentWeaponModel);

        currentWeapon = weapon;
        weaponHudTrans.transform.localPosition = currentWeapon.weaponHudLoc;
        currentWeaponModel = Instantiate(currentWeapon.weaponPrefab, weaponHudTrans.position, 
        currentWeapon.weaponHudRot.rotation, canvasTrans);
        currentWeapon.Setup(currentWeaponModel);
    }
}
