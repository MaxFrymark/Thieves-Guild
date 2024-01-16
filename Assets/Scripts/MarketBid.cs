using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketBid
{
    private CriminalCard card;
    public CriminalCard Card { get { return card; } }
    
    private Player playerMakingBid;
    public Player PlayerMakingBid { get { return playerMakingBid; } }

    private int currentBid;
    public int CurrentBid { get { return currentBid; } }
    private int bidLockedAt = 0;

    public MarketBid(CriminalCard card, Player playerMakingBid)
    {
        this.card = card;
        this.playerMakingBid = playerMakingBid;
        currentBid = 0;
    }

    public void IncrementBid()
    {
        currentBid++;
    }

    public bool DecrementBid()
    {
        if(currentBid <= bidLockedAt)
        {
            return false;
        }
        currentBid--;
        return true;
    }

    public void LockBid()
    {
        bidLockedAt = currentBid;
    }
}
