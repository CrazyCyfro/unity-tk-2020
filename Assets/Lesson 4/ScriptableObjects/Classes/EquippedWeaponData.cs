using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EquippedWeaponData", menuName = "ScriptableObjects/Equipped Weapon Data")]
public class EquippedWeaponData : ScriptableObject
{
    public FpsWeapon weapon; 
}
