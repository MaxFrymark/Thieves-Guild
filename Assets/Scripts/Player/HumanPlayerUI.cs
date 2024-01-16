using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayerUI : MonoBehaviour
{
    [SerializeField] PlayerHand playerHand;
    [SerializeField] TextMeshProUGUI coinCounter;
    private HumanPlayer humanPlayer;
    
    public void SetUpUI(HumanPlayer humanPlayer)
    {
        this.humanPlayer = humanPlayer;
    }

    public void UpdateCoinCounter(int coin)
    {
        coinCounter.text = "Coins: " + coin;
    }

    public bool AddCardToHand(CardControl card)
    {
        return playerHand.AddCardToHand(card);
    }

    public void RemoveCardFromHand(CardControl card)
    {
        playerHand.RemoveCardFromHand(card);
    }

    public void CancelPlay()
    {
        humanPlayer.CancelPlay();
    }
}
