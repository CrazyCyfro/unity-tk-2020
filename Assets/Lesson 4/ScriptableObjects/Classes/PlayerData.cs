using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "ScriptableObjects/Player Data")]

public class PlayerData : ScriptableObject
{   
    public bool Dead()
    {
        return currentHealth <= 0;
    }
    public int initHealth;
    public int currentHealth;
    public Vector3 playerPos;

    public int killCount;
}
