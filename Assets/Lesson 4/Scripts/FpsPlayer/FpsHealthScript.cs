using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsHealthScript : MonoBehaviour
{
    public PlayerData playerData;
    [Header("Health settings")]
    public int health;

    void Start()
    {
        health = playerData.initHealth;
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;

        Debug.Log("Health: " + health);
        
        if (health > 0) return;

        Debug.Log("You lose!");
    }
}
