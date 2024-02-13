using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    List<CriminalCard> criminalsInGraveyard = new List<CriminalCard>();

    public void AddCriminalToGraveyard(CriminalCard criminalCard)
    {
        criminalCard.transform.position = transform.position;
        criminalCard.transform.parent = transform;
        criminalsInGraveyard.Add(criminalCard);
    }

    public void RemoveCriminalFromGraveyard(CriminalCard criminalCard)
    {
        criminalsInGraveyard.Remove(criminalCard);
    }
}
