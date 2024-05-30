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
        var damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector3 dir = collision.transform.position - transform.position; //identifica a direção da colisao e calcula para onde mover o inimigo
            dir = -dir.normalized; //retorna o vetor para 1 independente da conta anterior
            dir.y = 0;

            damageable.Damage(damageAmount, dir);

        }
        Destroy(gameObject);
    }
}
