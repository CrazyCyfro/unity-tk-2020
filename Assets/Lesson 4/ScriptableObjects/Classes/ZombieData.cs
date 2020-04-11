using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New ZombieData", menuName = "ScriptableObjects/Zombie Data")]

public class ZombieData : ScriptableObject
{
    [Header("Main data")]
    public float speed;
    public int health;
    public int damage;
    public float attackSpd;
    public float destroyDelay;

    [Header("Sound data")]
    public AudioClip hurtClip;
    public AudioClip attackClip;
    public AudioClip idleClip;
    public float idleInterval;


}
