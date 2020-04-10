using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New ZombieData", menuName = "ScriptableObjects/Zombie Data")]

public class ZombieData : ScriptableObject
{
    public float speed;

    public int damage;

    public float attackSpd;
}
