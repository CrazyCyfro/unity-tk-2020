using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfpsEnemyHpScript : SfpsHealthBase
{
    public int initHealth;

    void Start()
    {
        health = initHealth;
    }
}
