using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt Life;
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textLife;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        Life.value = 3;
        textCoins.text = "x 0";
        textLife.text = "x 3";
    }

    public void AddCoin(int ammout = 1) //permite que em ocasioes especiais o ammout seja alterado quando a função é chamada Ex.: Addcoin(5) -> moeda agora vale 5
    {
        coins.value += ammout;
        textCoins.text = "x " + coins.value.ToString();
    }

    public void AddLife(int life = 1) //permite que em ocasioes especiais o ammout seja alterado quando a função é chamada Ex.: Addcoin(5) -> moeda agora vale 5
    {
        Life.value += life;
        textLife.text = "x " + Life.value.ToString();
    }
}
