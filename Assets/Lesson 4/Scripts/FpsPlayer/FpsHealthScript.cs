using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsHealthScript : MonoBehaviour
{
    public PlayerData playerData;
    [Header("Current health")]
    private int health;

    void Start()
    {
        health = playerData.initHealth;
    }

    void Update()
    {
        playerData.currentHealth = health;
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health < 0) health = 0;
    }
}
