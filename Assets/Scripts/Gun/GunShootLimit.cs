using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float maxAmmo = 5f;
    public float timeToReload = 1f;

    private float _currentShots;
    private bool _needReload = false;

    protected override IEnumerator ShootCouroutine()
    {


        if (_needReload) yield break; //nao deixa executar o while se need reload for true

        while (true)
        {
            if(_currentShots < maxAmmo)
            {
                Shoot();
                _currentShots++;
                CheckReload();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private void CheckReload()
    {
        if (_currentShots >= maxAmmo)
        {
            StopShoot();
            ReloadGun();
        }
    }


    private void ReloadGun()
    {
        _needReload = true;
        StartCoroutine(ReloadGunCourroutine());

    }

    IEnumerator ReloadGunCourroutine()
    {
        float time = 0;

        while (time < timeToReload)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _currentShots = 0;
        _needReload = false;

    }

}
