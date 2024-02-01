using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NeighborhoodUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI neighborhoodNameTextField;
    [SerializeField] TextMeshProUGUI coinsTextField;
    [SerializeField] TextMeshProUGUI wealthTextField;

    [SerializeField] GridLayoutGroup playerCriminalDisplay;
    [SerializeField] GridLayoutGroup opponentCriminalDisplay;
    List<DisplayForCardInNeighborhood> criminalsDisplayed = new List<DisplayForCardInNeighborhood>();

    
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

    public void AddCriminalToDisplay(CriminalCard card)
    {
        DisplayForCardInNeighborhood criminalDisplay = ObjectPool.Instance.GetObjectFromPool("Neighborhood Display").GetComponent<DisplayForCardInNeighborhood>();
        criminalDisplay.gameObject.SetActive(true);
        AssignCriminalToDisplay(card, criminalDisplay);
        criminalsDisplayed.Add(criminalDisplay);
        criminalDisplay.SetUpDisplay(card);
    }

    private void AssignCriminalToDisplay(CriminalCard card, DisplayForCardInNeighborhood criminalDisplay)
    {
        if (card.Owner is HumanPlayer)
        {
            criminalDisplay.transform.SetParent(playerCriminalDisplay.transform);
        }
        else
        {
            criminalDisplay.transform.SetParent(opponentCriminalDisplay.transform);
        }

    }

    public void RemoveCriminalFromDisplay(CriminalCard card)
    {
        DisplayForCardInNeighborhood displayToRemove = null;
        foreach(DisplayForCardInNeighborhood display in criminalsDisplayed)
        {
            if(card == display.AttachedCard)
            {
                displayToRemove = display;
                break;
            }
        }

        if(displayToRemove == null)
        {
            Debug.Log("Invalid Card Removed From Neighborhood");
        }
        criminalsDisplayed.Remove(displayToRemove);
        displayToRemove.ReturnToObjectPool();
    }
}
