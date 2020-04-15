using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FpsWeapon : MonoBehaviour
{
    public virtual void Setup(GameObject obj){}
    public virtual void AddAmmo(int amount){}
    public abstract bool CanFire();
    public abstract void Fire();

    public GameObject weaponPrefab;
    public AudioClip firingAudioClip;
    public Transform weaponHudRot;
    public Vector3 weaponHudPos;
    public int ammo;
    protected GameObject weaponObject;
    
}
