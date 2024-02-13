using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Neighborhood : MonoBehaviour
{

    [SerializeField] NeighborhoodUI neighborhoodUI;
    [SerializeField] NeighborhoodBorder neighborhoodBorder;
    

    City city;
        
    NeighborhoodType neighborhoodType;

    private List<CriminalCard> criminalsInNeighborHood = new List<CriminalCard>();
    public List<CriminalCard> CriminalsInNeighborHood { get { return criminalsInNeighborHood; } }

    int currentCoins = 0;
    int currentWealth;
    
    public void AssignNeighborhoodType(City city, NeighborhoodType neighborhoodType)
    {
        this.city = city;
        this.neighborhoodType = neighborhoodType;
        currentWealth = neighborhoodType.BaseWealth;
        UpdateCoinsForNewTurn();

        neighborhoodUI.UpdateNeighborhoodName(neighborhoodType.NeighborhoodName);
        neighborhoodUI.UpdateWealthTextField(currentCoins);
    }

    public void AddCriminalToNeighborhood(CriminalCard criminal)
    {
        criminalsInNeighborHood.Add(criminal);
        neighborhoodBorder.ReturnToBaseColor();
        neighborhoodUI.AddCriminalToDisplay(criminal);
    }

    public void RemoveCriminalFromNeighborhood(CriminalCard criminal)
    {
        criminalsInNeighborHood.Remove(criminal);
        criminal.SendToNeighborhood(null);
        neighborhoodUI.RemoveCriminalFromDisplay(criminal);
    }

    public void StartNewTurn()
    {
        UpdateCoinsForNewTurn();
    }

    public void UpdateCoinsForNewTurn()
    {
        currentCoins += currentWealth;
        neighborhoodUI.UpdateCoinsTextField(currentCoins);
    }

    public void CardEnteringNeighborhoodCollider(CardControl card)
    {
        city.AddNeighborhoodToTrackingList(card, this);
    }

    public void CardLeavingNeighborhoodCollider()
    {
        city.RemoveNeighborhoodFromTrackingList(this);
    }

    public void StealFromNeighborhood(int coinsToSteal, CriminalCard cardStealing)
    {
        for(int i = 0; i < coinsToSteal; i++)
        {
            if(currentCoins > 0)
            {
                currentCoins--;
                cardStealing.Owner.AddCoins(1);
            }
        }
        neighborhoodUI.UpdateCoinsTextField(currentCoins);
    }

    public void CriminalsAct()
    {
        foreach(CriminalCard card in criminalsInNeighborHood)
        {
            card.TakeAction(this);
        }
    }

    public void HighlightNeighborHood() => neighborhoodBorder.ChangeBorderColor(Color.yellow);
    public void RemoveHighlightNeighborhood() => neighborhoodBorder.ReturnToBaseColor();
}
