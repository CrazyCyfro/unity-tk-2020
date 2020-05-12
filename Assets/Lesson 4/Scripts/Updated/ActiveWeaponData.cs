using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActiveWeaponData", menuName = "ScriptableObjects/Active Weapon Data")]
public class ActiveWeaponData : ScriptableObject
{
    public string ammoString;
    public bool outOfAmmo;
    public bool reloading;

    
}
