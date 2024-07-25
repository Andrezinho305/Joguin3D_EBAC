using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenease = Ease.OutBack;
    private float startScale; 

    void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }



    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);

    }


    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if(p!=null)
        {
            ShowNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration).SetEase(tweenease);
    }


    public void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
        //notification.transform.DOScale(startScale, -tweenDuration).SetEase(tweenease).From();
    }
}