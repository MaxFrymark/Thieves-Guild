using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardPlay
{
    private CriminalCard card;
    public CriminalCard Card { get { return card; } }
    private Neighborhood neighborhood;
    public Neighborhood Neighborhood { get {  return neighborhood; } }

    public CardPlay(CriminalCard card, Neighborhood neighborhood)
    {
        this.card = card;
        this.neighborhood = neighborhood;
    }
}
