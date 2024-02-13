using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardState
{
    private CriminalCard card;

    public enum Location { Deck, Den, Neighborhood, Graveyard, Jail, Market }
    private Location currentLocation = Location.Deck;
    public Location CurrentLocation { get { return currentLocation; } }

    public delegate void CardLeftCurrentLocation(CriminalCard card);
    public event CardLeftCurrentLocation cardLeftCurrentLocation;

    public delegate void CardAddedToNewLocation(CriminalCard card);
    public event CardAddedToNewLocation cardAddedToNewLocation;

    public CardState(CriminalCard card)
    {
        currentLocation = Location.Deck;
        this.card = card;
    }

    public void ChangeLocation(Location newLocation)
    {
        if (cardLeftCurrentLocation != null)
        {
            cardLeftCurrentLocation.Invoke(card);
        }
        currentLocation = newLocation;
        if(cardAddedToNewLocation != null)
        {
            cardAddedToNewLocation.Invoke(card);
        }
    }

    
}
