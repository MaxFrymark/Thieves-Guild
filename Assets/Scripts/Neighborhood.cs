using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Neighborhood : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI neighborhoodNameTextField;
    [SerializeField] TextMeshProUGUI coinsTextField;
    [SerializeField] TextMeshProUGUI wealthTextField;
        
    NeighborhoodType neighborhoodType;

    List<CriminalCard> criminalsInNeighborHood = new List<CriminalCard>();

    int currentCoins;
    int currentWealth;
    
    public void AssignNeighborhoodType(NeighborhoodType neighborhoodType)
    {
        this.neighborhoodType = neighborhoodType;
        currentWealth = neighborhoodType.BaseWealth;
        currentCoins = currentWealth;

        neighborhoodNameTextField.text = neighborhoodType.NeighborhoodName;
        UpdateCoinsTextField();
        UpdateWealthTextField();
    }

    public void AddCriminalToNeighborhood(CriminalCard criminal)
    {
        criminalsInNeighborHood.Add(criminal);
    }

    public void StartNewTurn()
    {
        UpdateCoinsForNewTurn();
    }

    private void UpdateCoinsForNewTurn()
    {
        currentCoins += currentWealth;
    }

    private void UpdateCoinsTextField()
    {
        coinsTextField.text = "Coins: " + currentCoins;
    }

    private void UpdateWealthTextField()
    {
        wealthTextField.text = "Wealth: " + currentWealth;
    }
}
