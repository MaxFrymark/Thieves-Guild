using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<CriminalCard> criminalsInGraveyard = new List<CriminalCard>();
    public List<CriminalCard> CriminalsInGraveyard {  get { return criminalsInGraveyard; } }

    public void AddCriminalToGraveyard(CriminalCard criminalCard)
    {
        criminalCard.transform.parent = transform;
        criminalsInGraveyard.Add(criminalCard);
    }

    public void RemoveCriminalFromGraveyard(CriminalCard criminalCard)
    {
        criminalsInGraveyard.Remove(criminalCard);
    }
}
