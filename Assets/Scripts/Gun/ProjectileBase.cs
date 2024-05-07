using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 30f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {



    }
}