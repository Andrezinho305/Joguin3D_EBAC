using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Collectables;

public class ChestItem : ChestItemBase
{
    public int itemNumber = 5;
    public GameObject collectablePrefab;

    private List<GameObject> _items = new List<GameObject>();

    public Vector2 randomRange = new Vector2(-2f, 2f);

    public float tweenEndTime = .5f;

    public ItemType itemType = ItemType.COIN;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItem();
    }

    [NaughtyAttributes.Button]
    private void CreateItem()
    {
        for(int i = 0; i< itemNumber; i++)
        {
            var item = Instantiate(collectablePrefab);
            item.transform.position = transform.position + Vector3.forward*Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            //spawna o item de forma aleatória dentro da chest, utilizar quando houverem varios
            item.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var i in _items)
        {
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);

            ItemManager.Instance.AddByType(itemType);
        }
    }

}
