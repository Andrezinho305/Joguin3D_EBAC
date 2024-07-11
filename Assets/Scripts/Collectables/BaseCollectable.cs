using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 1f;
    public GameObject graphicItem;
    public Collider collider;

    public AudioSource audioSource;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        gameObject.SetActive(false);
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollectCoins();
        OnCollectLife();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollectCoins()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, 2f);
        }

        if (audioSource != null) audioSource.Play();
    }

    protected virtual void OnCollectLife()
    {

    }

}
