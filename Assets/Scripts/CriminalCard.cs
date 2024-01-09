using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CriminalCard : MonoBehaviour
{
    [SerializeField] CardUI cardUI;
    [SerializeField] CardControl cardControl;

    public CardControl CardControl {  get { return cardControl; } }
    
    CriminalType criminalType;

    Player owner;
    public Player Owner { get { return owner; } }

    Neighborhood currentNeighborhood;

    public enum Location { Deck, Den, City, Graveyard, Jail, Market}
    private Location currentLocation = Location.Deck;
    public Location CurrentLocation { get { return currentLocation; } }

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
        owner.RemoveCriminalFromDen(this);
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

    public void PickUpCard()
    {
        cardControl.PickUpCard();
    }

    public void ReleaseCard()
    {
        cardControl.ReleaseCard();
    }
}
