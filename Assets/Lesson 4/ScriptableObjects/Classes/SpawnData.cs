using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnData", menuName = "ScriptableObjects/Spawn Data")]
public class SpawnData : ScriptableObject
{
    public int zombies;
    public int resupply;
}
