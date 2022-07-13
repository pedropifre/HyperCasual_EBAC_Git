using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class CoinsCount : Singleton<CoinsCount>
{
    public TextMeshProUGUI textoCoin;
    private int coinsCount;
    public LevelManager levelManager;

    void Start()
    {
        if (levelManager._index == 0)
        {
            coinsCount = 0;
        }

    }

    public void ChangeTextCoins(int val)
    {
        coinsCount += val;
        textoCoin.text = coinsCount.ToString() + " Coins";
    }

    public void ChangeTextZerarCoins(int val = 0)
    {
        coinsCount = val;
        textoCoin.text = coinsCount.ToString() + " Coins";
    }
}
