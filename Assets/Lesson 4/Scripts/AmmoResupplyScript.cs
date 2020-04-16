using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoResupplyScript : MonoBehaviour
{
    public SpawnData spawnData;
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag != "Player") return;

        FpsFiringScript player = collider.gameObject.GetComponent<FpsFiringScript>();

        foreach (FpsWeapon weapon in player.weapons) {
            weapon.ResetAmmo();
        }
        spawnData.resupply--;

        Destroy(gameObject);
    }
}
