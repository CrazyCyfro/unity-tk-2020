using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireMechanism : MonoBehaviour
{
    public enum FireMode
    {
        Semi,
        Auto
    }

    public FireMode mode;

    protected float cooldown;
    private float t = 0;
    public bool CooledDown()
    {
        if (Time.time - t > cooldown)
        {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    public abstract void Fire();
}
