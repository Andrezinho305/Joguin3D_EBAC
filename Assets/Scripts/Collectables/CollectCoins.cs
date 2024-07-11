using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : BaseCollectable
{

    protected override void OnCollectCoins()
    {
        base.OnCollectCoins();
        ItemManager.Instance.AddCoin();
        collider.enabled = false;
    }

}
