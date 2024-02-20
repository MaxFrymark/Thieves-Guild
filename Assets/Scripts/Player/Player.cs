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

    private List<CriminalCard> activeCriminals = new List<CriminalCard>();
    private List<CriminalCard> criminalsToSetTarget = new List<CriminalCard>();

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

    public virtual void SetTargetsForCardsToPlay()
    {
        criminalsToSetTarget.Clear();
    }

    public void TakeFastActions()
    {
        foreach(CriminalCard card in activeCriminals)
        {
            if (card.CriminalType.Tags.Contains(CriminalType.CriminalTags.Fast))
            {
                card.CriminalType.TakeAction();
            }
        }
    }

    public void TakeActions()
    {
        foreach (CriminalCard card in activeCriminals)
        {
            if (!card.CriminalType.Tags.Contains(CriminalType.CriminalTags.Fast))
            {
                card.CriminalType.TakeAction();
            }
        }
    }

    public void ClearPlays()
    {
        cardPlays.Clear();
    }

    public void AddToActiveCriminals(CriminalCard card)
    {
        activeCriminals.Add(card);
        criminalsToSetTarget.Add(card);
    }

    public void RemoveFromActiveCriminals(CriminalCard card)
    {
        if (activeCriminals.Contains(card))
        {
            activeCriminals.Remove(card);
        }

        else
        {
            Debug.Log("Error: Attemped to remove card from active list that wasn't in the list");
        }

        if (criminalsToSetTarget.Contains(card))
        {
            criminalsToSetTarget.Remove(card);
        }
    }
}
