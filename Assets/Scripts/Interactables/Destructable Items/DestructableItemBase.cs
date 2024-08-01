using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 5;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }


    private void OnDamage(HealthBase h) //need tag to be "Enemy", looking for a bug fix
    {

        transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);

    }



}
