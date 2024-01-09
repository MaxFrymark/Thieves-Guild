using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState
{
    public enum Location { Deck, Den, City, Graveyard, Jail, Market }
    private Location currentLocation = Location.Deck;
    public Location CurrentLocation { get { return currentLocation; } }

    public CardState()
    {
        currentLocation = Location.Deck;
    }

    public void ChangeLocation(Location newLocation)
    {
        currentLocation = newLocation;
    }
}
