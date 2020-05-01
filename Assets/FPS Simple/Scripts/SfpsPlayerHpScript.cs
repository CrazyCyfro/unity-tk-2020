using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfpsPlayerHpScript : SfpsHealthBase
{
    public int initHealth;

    void Start()
    {
        health = initHealth;
    }
    public override void Die()
    {
        Debug.Log("I seem to be dead");
    }
}
