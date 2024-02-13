using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CriminalCard criminalCardPrefab;
    [SerializeField] Transform deckLocation;
    [SerializeField] Transform jailLocation;
    [SerializeField] Graveyard graveyard;
    [SerializeField] Market market;

    List<CriminalCard> deck = new List<CriminalCard>();

    private void Start()
    {
        BuildDeck();
        SendCardsToMarket();
    }

    public CriminalCard CreateCard()
    {
        CriminalCard card = ObjectPool.Instance.GetObjectFromPool("Card").GetComponent<CriminalCard>();
        card.transform.SetParent(transform);
        card.gameObject.SetActive(true);
        card.SetUpCardBasics();
        card.CardState.cardLeftCurrentLocation += OnCardRemovedFromLocation;
        card.CardState.cardAddedToNewLocation += OnCardAddedToLocation;
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
        for(int i = 0; i < 5; i++)
        {
            CriminalCard card = CreateCard();
            deck.Add(card);
            card.AssignCriminalType(new Assassin());
            card.transform.position += deckLocation.transform.position;
        }
    }

    public void SendCardsToMarket()
    {
        bool isSpaceInMarket = market.CheckIfSpaceInMarket();
        CriminalCard cardToSend = null;
        while(isSpaceInMarket)
        {
            cardToSend = deck[UnityEngine.Random.Range(0, deck.Count)];
            cardToSend.SendToMarket();
            isSpaceInMarket = market.CheckIfSpaceInMarket();
        }
        ReturnCardToDeck(cardToSend);
    }

    public void ReturnCardToDeck(CriminalCard card)
    {
        card.transform.position = deckLocation.position;
        card.SendToDeck();
        deck.Add(card);
    }

    private void OnCardRemovedFromLocation(CriminalCard card)
    {
        card.transform.position = deckLocation.position;
        card.transform.parent = transform;
        switch (card.CardState.CurrentLocation)
        {
            case CardState.Location.Deck:
                deck.Remove(card);
                break;
            case CardState.Location.Den:
                card.Owner.RemoveCriminalFromDen(card);
                break;
            case CardState.Location.Neighborhood:
                City.Instance.RemoveCriminalFromNeighborhood(card);
                break;
            case CardState.Location.Market:
                break;
            case CardState.Location.Jail:
                break;
            case CardState.Location.Graveyard:
                graveyard.RemoveCriminalFromGraveyard(card);
                break;
        }
    }

    private void OnCardAddedToLocation(CriminalCard card)
    {
        switch(card.CardState.CurrentLocation)
        {
            case CardState.Location.Deck:
                break;
            case CardState.Location.Den:
                break;
            case CardState.Location.Neighborhood:
                break;
            case CardState.Location.Market:
                market.DealCardToMarket(card);
                break;
            case CardState.Location.Jail:
                break;
            case CardState.Location.Graveyard:
                graveyard.AddCriminalToGraveyard(card);
                break;
        }
    }
}
