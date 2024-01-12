using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] Transform[] cardSlots;
    CriminalCard[] cardsInMarket = new CriminalCard[6];
    List<MarketBid> currentBids = new List<MarketBid>();


    public bool DealCardToMarket(CriminalCard newCard)
    {
        for(int i = 0; i < cardsInMarket.Length; i++)
        {
            if (cardsInMarket[i] == null)
            {
                cardsInMarket[i] = newCard;
                newCard.transform.position = cardSlots[i].transform.position;
                newCard.SendToMarket();
                return false;
            }
        }

        return true;
    }
}
