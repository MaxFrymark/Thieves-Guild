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
    
    private CriminalType criminalType;
    public CriminalType CriminalType { get { return criminalType; } }

    Player owner;
    public Player Owner { get { return owner; } }

    private Neighborhood currentNeighborhood;
    public Neighborhood CurrentNeighborhood { get {  return currentNeighborhood; } }

    

    public void AssignCriminalType(CriminalType criminalType)
    {
        cardState = new CardState();
        this.criminalType = criminalType;
        cardUI.SetUpCardUI(criminalType);
        criminalType.SetAttachedCard(this);
    }

    private void AssignOwner(Player owner)
    {
        this.owner = owner;
        
    }

    public void SetNeighborhood(Neighborhood neighborhood)
    {
        currentNeighborhood = neighborhood;
        if(neighborhood != null)
        {
            AssignNeighborhood(neighborhood);
        }
    }

    private void AssignNeighborhood(Neighborhood neighborhood)
    {
        owner.RemoveCriminalFromDen(this);
        owner.AddCardPlay(new CardPlay(this, neighborhood));
        cardState.ChangeLocation(CardState.Location.City);
        neighborhood.AddCriminalToNeighborhood(this);
    }

    public void SendToDen(Player owner)
    {
        if(owner != this.Owner)
        {
            this.owner = owner;
        }
        cardState.ChangeLocation(CardState.Location.Den);
    }

    public void SendToMarket()
    {
        cardState.ChangeLocation(CardState.Location.Market);
    }

    public void SendToDeck()
    {
        cardState.ChangeLocation(CardState.Location.Deck);
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

    public void SetToPickedUpLayer() => cardUI.SetToPickedUpLayer();
    public void SetToBaseCardLayer() => cardUI.SetToBaseCardLayer();
    public void SetCardImage(Sprite sprite) => cardUI.SetCardImage(sprite);
    public void TakeAction(Neighborhood neighborhood) => criminalType.TakeAction(neighborhood);
}
