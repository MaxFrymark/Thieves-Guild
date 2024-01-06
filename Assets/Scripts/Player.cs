using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    List<CriminalCard> den;
    int coins;

    public virtual void AddCoins(int additionalCoins)
    {
        coins += additionalCoins;
    }

    public virtual void AddCriminalToDen(CriminalCard criminal)
    {
        criminal.AssignOwner(this);
        den.Add(criminal);
    }
}
