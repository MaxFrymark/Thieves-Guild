using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected List<CriminalCard> den = new List<CriminalCard>();
    protected int coins;

    public virtual void AddCoins(int additionalCoins)
    {
        coins += additionalCoins;
    }

    public virtual void AddCriminalToDen(CriminalCard criminal)
    {
        criminal.AssignOwner(this);
        den.Add(criminal);
    }

    public virtual void RemoveCriminalFromDen(CriminalCard criminal)
    {
        den.Remove(criminal);
    }

    public virtual bool SpendCoin()
    {
        return false;
    }
}
