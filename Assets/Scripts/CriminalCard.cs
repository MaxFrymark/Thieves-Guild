using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CriminalCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI criminalNameTextField;
    [SerializeField] TextMeshProUGUI criminalTypeTextField;
    [SerializeField] TextMeshProUGUI criminalTagsField;
    [SerializeField] TextMeshProUGUI criminalDescriptionTextField;
    [SerializeField] TextMeshProUGUI strengthTextField;
    
    CriminalType criminalType;
    Player owner;
    Neighborhood currentNeighborhood;

    private enum Location { Deck, Den, City, Graveyard, Jail, Market}
    private Location currentLocation = Location.Deck;

    public void AssignThiefType(CriminalType criminalType)
    {
        this.criminalType = criminalType;
        criminalNameTextField.text = criminalType.CriminalName;
        switch(criminalType)
        {
            case GuildMember:
                criminalTypeTextField.text = "Member";
                break;
            case Agent:
                criminalTypeTextField.text = "Agent";
                break;
        }
        criminalDescriptionTextField.text = criminalType.CriminalDescription;
        criminalTagsField.text = "";
        if(criminalType.Tags.Count > 0)
        {
            foreach(var tag in criminalType.Tags)
            {
                criminalTagsField.text += tag.ToString();
            }
        }
        strengthTextField.text = criminalType.Strength.ToString();
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
