using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeighborhoodUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI neighborhoodNameTextField;
    [SerializeField] TextMeshProUGUI coinsTextField;
    [SerializeField] TextMeshProUGUI wealthTextField;

    
    public void UpdateNeighborhoodName(string neighborhoodName)
    {
        neighborhoodNameTextField.text = neighborhoodName;
    }

    public void UpdateCoinsTextField(int currentCoins)
    {
        coinsTextField.text = "Coins: " + currentCoins;
    }

    public void UpdateWealthTextField(int currentWealth)
    {
        wealthTextField.text = "Wealth: " + currentWealth;
    }
}
