using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "ScriptableObjects/Player Data")]

public class PlayerData : ScriptableObject
{
    public int initHealth;
    public Vector3 playerPos;
}
