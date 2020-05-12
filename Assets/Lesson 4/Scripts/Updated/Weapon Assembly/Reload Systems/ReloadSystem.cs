using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ReloadSystem : MonoBehaviour
{
    public abstract bool CanFire();
    public abstract void Fired();
    public abstract void Reload();
    public abstract void CancelReload();
}
