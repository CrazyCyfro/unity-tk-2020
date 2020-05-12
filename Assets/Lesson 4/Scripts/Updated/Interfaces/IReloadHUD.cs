using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadHUD
{
    bool OutOfAmmo();
    bool Reloading();
    string AmmoString();

}
