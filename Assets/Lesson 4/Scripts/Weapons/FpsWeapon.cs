using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FpsWeapon : MonoBehaviour
{

    // Run when swapping to weapon
    public virtual void Setup(GameObject obj) {
        weaponObject = obj;
    }

    // Run when swapping away from weapon
    public virtual void Dismantle() {}
    public virtual void AddAmmo(int amount) {
        ammo += amount;
    }

    public virtual void Reload() {}

    public virtual bool IsReloading()
    {
        return false;
    }

    // Accounts for ammo, reload and time interval to check if weapon can fire
    public virtual bool CanFire()
    {
        if (!HasAmmo()) return false;
        if (IsReloading()) return false;

        if (Time.time - t > fireInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    public virtual bool HasAmmo()
    {
        if (ammo <= 0) return false;
        return true;
    }

    public virtual string AmmoString()
    {
        return ammo.ToString();
    }
    public abstract void Fire();

    // HUD Weapon Prefab
    public GameObject weaponPrefab;

    public AudioClip firingAudioClip;
    
    // Rotation reference for HUD weapon (usually Main Camera)
    public Transform weaponHudRot;

    // Local xyz position inside HUD
    public Vector3 weaponHudPos;
    public int ammo;
    public float fireInterval;

    // HUD GameObject representation of weapon
    protected GameObject weaponObject;
    protected float t;
    
}
