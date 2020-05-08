using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagReserveSystem : ReloadSystem, IAddReset
{
    public int reserve;
    public int magSize;
    public float reloadDuration;

    private int mag;
    private int initReserve;
    private bool reloading =  false;
    private IEnumerator reloadCoroutine;

    void Awake()
    {
        initReserve = reserve;
        mag = magSize;
    }
    void OnEnable()
    {
        if (reserve > 0 && mag == 0) Reload();
    }

    void OnDisable()
    {
        CancelReload();
    }
    public override bool CanFire()
    {
        if (mag == 0) return false;
        if (reloading) return false;

        return true;
    }

    public override void Fired()
    {
        mag -= 1;
        if (reserve > 0 && mag == 0) Reload();
    }

    public override void Reload()
    {
        if (reloading) return;
        if (mag == magSize) return;
        if (reserve == 0) return;

        reloadCoroutine = ReloadTimer();
        StartCoroutine(reloadCoroutine);
    }

    public override void CancelReload()
    {
        if (reloadCoroutine == null) return;
        StopCoroutine(reloadCoroutine);
        reloading = false;
    }

    IEnumerator ReloadTimer()
    {
        reloading = true;
        
        yield return new WaitForSeconds(reloadDuration);

        int deficit = magSize - mag;
        if (reserve >= deficit) {
            reserve -= deficit;
            mag += deficit;
        } else {
            mag += reserve;
            reserve = 0;
        }

        reloading = false;
    }

    

    public void Add(int a)
    {
        reserve += a;
    }

    public void Reset()
    {
        reserve = initReserve;
    }
}
