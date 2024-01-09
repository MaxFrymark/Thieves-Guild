using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketBid
{
    private Player playerMakingBid;
    public Player PlayerMakingBid { get { return playerMakingBid; } }

    private int currentBid;
    public int CurrentBid { get { return currentBid; } }

    public MarketBid(Player playerMakingBid, int currentBid)
    {
        this.playerMakingBid = playerMakingBid;
        currentBid = 1;
    }

    public void IncrementBid()
    {
        currentBid++;
    }

    public bool DecrementBid()
    {
        if(currentBid <= 0)
        {
            return false;
        }
        currentBid--;
        return true;
    }
}
