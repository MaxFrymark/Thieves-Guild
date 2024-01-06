using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CriminalCard criminalCardPrefab;

    List<CriminalCard> deck = new List<CriminalCard>();
}
