using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayerUI
{
    PlayerHand playerHand;
    TextMeshProUGUI coinCounter;  
    
    public void SetUpUI()
    {
        playerHand = GameObject.FindAnyObjectByType<PlayerHand>();
        GameObject counter = GameObject.FindGameObjectWithTag("Coin Counter");
        coinCounter = counter.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinCounter(int coin)
    {
        coinCounter.text = "Coins: " + coin;
    }

    public bool AddCardToHand(GameObject card)
    {
        return playerHand.AddCardToHand(card);
    }

    public void RemoveCardFromHand(GameObject card)
    {
        playerHand.RemoveCardFromHand(card);
    }
}
