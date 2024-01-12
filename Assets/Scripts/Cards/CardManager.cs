using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CriminalCard criminalCardPrefab;
    [SerializeField] Transform deckLocation;
    [SerializeField] Market market;

    List<CriminalCard> deck = new List<CriminalCard>();

    private void Start()
    {
        BuildDeck();
        SendCardsToMarket();
    }

    public CriminalCard CreateCard()
    {
        CriminalCard card = Instantiate(criminalCardPrefab, transform);
        return card;
    }

    private void BuildDeck()
    {
        for(int i = 0; i < 10; i++)
        {
            CriminalCard card = CreateCard();
            deck.Add(card);
            card.AssignCriminalType(new Thief());
            card.transform.position = deckLocation.transform.position;
        }
    }

    public void SendCardsToMarket()
    {
        bool isMarketFull = false;
        CriminalCard cardToSend = null;
        while(!isMarketFull)
        {
            cardToSend = deck[Random.Range(0, deck.Count)];
            deck.Remove(cardToSend);
            isMarketFull = market.DealCardToMarket(cardToSend);
        }
        ReturnCardToDeck(cardToSend);
    }

    public void ReturnCardToDeck(CriminalCard card)
    {
        card.SendToDeck();
        deck.Add(card);
    }
}
