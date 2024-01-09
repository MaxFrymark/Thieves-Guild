using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CriminalCard : MonoBehaviour
{
    [SerializeField] CardUI cardUI;
    [SerializeField] CardControl cardControl;
    CardState cardState;

    public CardControl CardControl {  get { return cardControl; } }
    
    CriminalType criminalType;

    Player owner;
    public Player Owner { get { return owner; } }

    Neighborhood currentNeighborhood;

    

    public void AssignThiefType(CriminalType criminalType)
    {
        cardState = new CardState();
        this.criminalType = criminalType;
        cardUI.SetUpCardUI(criminalType);
    }

    public void AssignOwner(Player owner)
    {
        this.owner = owner;
        cardState.ChangeLocation(CardState.Location.Den);
    }

    public void AssignNeighborhood(Neighborhood neighborhood)
    {
        currentNeighborhood = neighborhood;
        owner.RemoveCriminalFromDen(this);
        cardState.ChangeLocation(CardState.Location.City);
    }

    public void PickUpCard()
    {
        cardControl.PickUpCard();
    }

    public void ReleaseCard()
    {
        cardControl.ReleaseCard();
    }

    public CardState.Location GetCardStateLocation()
    {
        return cardState.CurrentLocation;
    }
}
