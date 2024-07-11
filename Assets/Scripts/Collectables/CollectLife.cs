using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLife : BaseCollectable
{

    protected override void OnCollectLife()
    {
        base.OnCollectLife();
        ItemManager.Instance.AddLife(); 
        collider.enabled = false;
    }

}
