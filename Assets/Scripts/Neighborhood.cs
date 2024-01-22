using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Neighborhood : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardDisplayPrefab;

    [SerializeField] NeighborhoodUI neighborhoodUI;
    [SerializeField] NeighborhoodBorder neighborhoodBorder;

    City city;
        
    NeighborhoodType neighborhoodType;

    List<CriminalCard> criminalsInNeighborHood = new List<CriminalCard>();
    List<TextMeshProUGUI> cardsDisplayed = new List<TextMeshProUGUI>();

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
        TextMeshProUGUI cardDisplay = Instantiate(cardDisplayPrefab, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity, neighborhoodUI.transform);
        cardDisplay.text = criminal.CriminalType.CriminalName;
        cardDisplay.color = criminal.Owner.PlayerColor;
        cardsDisplayed.Add(cardDisplay);
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
