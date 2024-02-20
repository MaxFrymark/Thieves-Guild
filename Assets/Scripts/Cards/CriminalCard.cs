using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CriminalCard : MonoBehaviour
{
    [SerializeField] CardUI cardUI;
    [SerializeField] CardControl cardControl;
    [SerializeField] GameObject cardHighlight;
    CardState cardState;
    public CardState CardState { get { return cardState; } }

    public CardControl CardControl {  get { return cardControl; } }
    
    private CriminalType criminalType;
    public CriminalType CriminalType { get { return criminalType; } }

    Player owner;
    public Player Owner { get { return owner; } }

    private Neighborhood currentNeighborhood;
    public Neighborhood CurrentNeighborhood { get {  return currentNeighborhood; } }

    public void SetUpCardBasics()
    {
        cardState = new CardState(this);
    }

    public void AssignCriminalType(CriminalType criminalType)
    {
        this.criminalType = criminalType;
        cardUI.SetUpCardUI(criminalType);
    }

    public void SendToNeighborhood(Neighborhood neighborhood)
    {
        currentNeighborhood = neighborhood;
        if(neighborhood != null)
        {
            AssignNeighborhood(neighborhood);
        }
    }

    private void AssignNeighborhood(Neighborhood neighborhood)
    {
        owner.AddCardPlay(new CardPlay(this, neighborhood));
        cardState.ChangeLocation(CardState.Location.Neighborhood);
        neighborhood.AddCriminalToNeighborhood(this);
    }

    public void SendToDen(Player owner)
    {
        if(owner != this.Owner)
        {
            this.owner = owner;
        }
        cardState.ChangeLocation(CardState.Location.Den);
        owner.AddCriminalToDen(this);
    }

    public void SendToMarket()
    {
        cardState.ChangeLocation(CardState.Location.Market);
    }

    public void SendToDeck()
    {
        cardState.ChangeLocation(CardState.Location.Deck);
    }

    public void SendToGraveyard()
    {
        cardState.ChangeLocation(CardState.Location.Graveyard);
        owner = null;
    }


    public CardState.Location GetCardStateLocation()
    {
        return cardState.CurrentLocation;
    }

    public void SetCardHighlight(bool isHighlightEnabled)
    {
        cardHighlight.SetActive(isHighlightEnabled);
    }

    public void PickUpCard() => cardControl.PickUpCard();

    public void ReleaseCard() => cardControl.ReleaseCard();

    public void SetToPickedUpLayer() => cardUI.SetToPickedUpLayer();
    public void SetToBaseCardLayer() => cardUI.SetToBaseCardLayer();
    public void SetCardImage(Sprite sprite) => cardUI.SetCardImage(sprite);
    public void TakeAction() => criminalType.TakeAction();
    public void PlayCardToNeighborhood(Neighborhood neighborhood) => cardControl.PlayToNeighborhood(neighborhood);
    public bool CheckIfAICanPlay(Neighborhood neighborhood) => criminalType.CheckIfAICanPlay(neighborhood);
    public TargetRule GetTargetRule() => CriminalType.TargetRule;

    
}
