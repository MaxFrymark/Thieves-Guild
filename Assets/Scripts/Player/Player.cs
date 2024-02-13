using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected List<CriminalCard> den = new List<CriminalCard>();
    protected int coins;

    protected List<CardPlay> cardPlays = new List<CardPlay>();

    private Color playerColor;
    public Color PlayerColor {  get { return playerColor; } }

    public Player(Color playerColor)
    {
        this.playerColor = playerColor;
    }

    public virtual void AddCoins(int additionalCoins)
    {
        coins += additionalCoins;
    }

    public virtual void AddCriminalToDen(CriminalCard criminal)
    {
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

    public void AddCardPlay(CardPlay play)
    {
        cardPlays.Add(play);
    }

    public void SetTargetsForCardsToPlay()
    {
        foreach(CardPlay play in cardPlays)
        {
            
        }
    }

    public void TakeFastActions()
    {
        foreach (CardPlay play in cardPlays)
        {

        }
    }

    public void TakeActions()
    {
        foreach (CardPlay play in cardPlays)
        {

        }
    }

    public void ClearPlays()
    {
        cardPlays.Clear();
    }
}
