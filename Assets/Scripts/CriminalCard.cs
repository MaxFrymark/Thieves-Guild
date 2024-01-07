using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CriminalCard : MonoBehaviour
{
    [SerializeField] CardUI cardUI;
    
    CriminalType criminalType;
    Player owner;
    Neighborhood currentNeighborhood;

    private enum Location { Deck, Den, City, Graveyard, Jail, Market}
    private Location currentLocation = Location.Deck;

    public void AssignThiefType(CriminalType criminalType)
    {
        this.criminalType = criminalType;
        cardUI.SetUpCardUI(criminalType);
    }

    public void AssignOwner(Player owner)
    {
        this.owner = owner;
        MoveToDen();
    }

    public void AssignNeighborhood(Neighborhood neighborhood)
    {
        currentNeighborhood = neighborhood;
        MoveToCity();
    }

    private void MoveToMarket()
    {
        if(currentLocation == Location.Deck)
        {
            currentLocation = Location.Market;
        }
    }

    private void MoveToDen()
    {
        currentLocation = Location.Den;
        currentNeighborhood = null;
    }

    private void MoveToCity()
    {
        currentLocation = Location.City;
    }

    private void MoveToGraveyard()
    {
        currentLocation = Location.Graveyard;
        owner = null;
        currentNeighborhood = null;
    }

    private void MoveToJail()
    {
        currentLocation = Location.Jail;
        owner = null;
        currentNeighborhood = null;
    }

    private void MoveToDeck()
    {
        currentLocation = Location.Deck;
        owner = null;
        currentNeighborhood = null;
    }
}
