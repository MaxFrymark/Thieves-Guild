using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    
    [SerializeField] Transform[] cardSlots;
    [SerializeField] MarketUI marketUI;
    [SerializeField] InputHandler inputHandler;
    CriminalCard[] cardsInMarket = new CriminalCard[6];

    List<MarketBid> currentBids = new List<MarketBid>();

    MarketBid workingBid = null;

    public bool DealCardToMarket(CriminalCard newCard)
    {
        for(int i = 0; i < cardsInMarket.Length; i++)
        {
            if (cardsInMarket[i] == null)
            {
                cardsInMarket[i] = newCard;
                newCard.transform.position = cardSlots[i].transform.position;
                newCard.SendToMarket();
                return false;
            }
        }

        return true;
    }

    public void OpenBidDisplay(CriminalCard cardToBidOn, Player biddingPlayer)
    {
        workingBid = null;
        marketUI.SetBiddingDisplayActive(true);
        marketUI.SetUpDisplayCard(cardToBidOn);
        foreach(MarketBid bid in currentBids)
        {
            if(cardToBidOn == bid.Card)
            {
                workingBid = bid;
            }
        }

        if (workingBid == null)
        {
            SetUpWorkingBid(cardToBidOn, biddingPlayer);
        }

        marketUI.UpdateDisplayCardBidCounter(workingBid.CurrentBid);
    }

    public void TakeBidFromAI(int cardIndex, int bidAmount, Player biddingPlayer)
    {
        workingBid = new MarketBid(cardsInMarket[cardIndex], biddingPlayer);
        for(int i = 0; i < bidAmount; i++)
        {
            workingBid.PlayerMakingBid.SpendCoin();
            workingBid.IncrementBid();
        }
        currentBids.Add(workingBid);
        workingBid = null;
    }

    private void SetUpWorkingBid(CriminalCard cardToBidOn, Player biddingPlayer)
    {
        if(currentBids.Count > 0)
        {
            foreach(MarketBid bid in currentBids)
            {
                if(bid.PlayerMakingBid == biddingPlayer && bid.Card == cardToBidOn)
                {
                    workingBid = bid;
                    return;
                } 
            }
        }

        workingBid = new MarketBid(cardToBidOn, biddingPlayer);
    }

    public void CloseBidDisplay()
    {
        marketUI.SetBiddingDisplayActive(false);
        int cardIndex = 0;
        for(int i = 0; i < cardsInMarket.Length; i++)
        {
            if (cardsInMarket[i] == workingBid.Card)
            {
                cardIndex = i;
            }
        }
        marketUI.UpdateBidCounters(cardIndex, workingBid.CurrentBid);
        if(workingBid.CurrentBid > 0)
        {
            currentBids.Add(workingBid);
        }
        else
        {
            if (currentBids.Contains(workingBid))
            {
                currentBids.Remove(workingBid);
            }
        }
        workingBid = null;
        inputHandler.ReturnToNormalControl();
    }

    

    public void IncreaseBidButtonPressed()
    {
        if (workingBid.PlayerMakingBid.SpendCoin())
        {
            workingBid.IncrementBid();
            marketUI.UpdateDisplayCardBidCounter(workingBid.CurrentBid);
        }
    }

    public void DecreaseBidButtonPressed()
    {
        if (workingBid.DecrementBid())
        {
            workingBid.PlayerMakingBid.AddCoins(1);
            marketUI.UpdateDisplayCardBidCounter(workingBid.CurrentBid);
        }
    }

    public void ResolveBids()
    {
        if(currentBids.Count > 0)
        {
            List<MarketBid> bidsToResolve = new List<MarketBid>();
            foreach(MarketBid bid in currentBids)
            {
                bidsToResolve.Add(bid);
            }
            ResolveBids(bidsToResolve);
        }
    }

    private void ResolveBids(List<MarketBid> bidsToResolve)
    {
        if (bidsToResolve.Count > 0)
        {
            if (bidsToResolve.Count > 1)
            {
                CompareToOtherBids(bidsToResolve);
            }

            else
            {
                PlayerWinsBid(bidsToResolve[0]);
                bidsToResolve.Clear();
            }
        }
    }

    private void CompareToOtherBids(List<MarketBid> bidsToResolve)
    {
        MarketBid bidToResolve = bidsToResolve[0];
        List<MarketBid> competingBids = new List<MarketBid>();
        for(int i = 1; i < bidsToResolve.Count; i++)
        {
            if (bidsToResolve[i].Card == bidToResolve.Card)
            {
                competingBids.Add(bidsToResolve[i]);
            }
        }

        if(competingBids.Count == 0)
        {
            PlayerWinsBid(bidToResolve);
            bidsToResolve.Remove(bidToResolve);
            ResolveBids(bidsToResolve);
        }
        else
        {
            competingBids.Add(bidToResolve);
            ResolveCompetingBids(bidsToResolve, competingBids);
        }
    }

    private void ResolveCompetingBids(List<MarketBid> bidsToResolve, List<MarketBid> competingBids)
    {
        Debug.Log("Bids to Resolve: " + bidsToResolve.Count);
        int highestBid = 0;
        foreach(MarketBid bid in competingBids)
        {
            if(bid.CurrentBid > highestBid)
            {
                highestBid = bid.CurrentBid;
                bidsToResolve.Remove(bid);
            }
        }
        List<MarketBid> highestBids = new List<MarketBid>();
        foreach(MarketBid bid in competingBids)
        {
            if(bid.CurrentBid == highestBid)
            {
                Debug.Log("High bid: " + bid.CurrentBid +", " + bid.PlayerMakingBid);
                highestBids.Add(bid);
            }
            else
            {
                Debug.Log("Low bid: " + bid.CurrentBid + ", " + bid.PlayerMakingBid);
                bidsToResolve.Remove(bid);
                PlayerLosesBid(bid);
            }
        }

        if(highestBids.Count > 1)
        {
            foreach(MarketBid bid in highestBids)
            {
                bidsToResolve.Remove(bid);
                LockBid(bid);
            }
        }

        else
        {
            PlayerWinsBid(highestBids[0]);
        }


        ResolveBids(bidsToResolve);
    }

    private void PlayerWinsBid(MarketBid bid)
    {
        Debug.Log("Winner: " + bid.PlayerMakingBid);
        SendCardToPlayer(bid);
        currentBids.Remove(bid);
    }

    private void PlayerLosesBid(MarketBid bid)
    {
        Debug.Log("Loser: " + bid.PlayerMakingBid);

        bid.PlayerMakingBid.AddCoins(bid.CurrentBid);
        currentBids.Remove(bid);
    }

    private void LockBid(MarketBid bid)
    {
        Debug.Log("Locked");
        bid.LockBid();
    }

    private void SendCardToPlayer(MarketBid bid)
    {
        int index = 0;
        for (int i = 0; i < cardsInMarket.Length; i++)
        {
            if (cardsInMarket[i] == bid.Card)
            {
                index = i;
            }
        }

        marketUI.UpdateBidCounters(index, 0);
        cardsInMarket[index] = null;
        
        SendCardToPlayer(bid.PlayerMakingBid, bid.Card);
    }

    private void SendCardToPlayer(Player player, CriminalCard card)
    {
        player.AddCriminalToDen(card);
    }

    public void ClearMarket()
    {
        for(int i = 0; i < cardsInMarket.Length; i++)
        {
            if (cardsInMarket[i] != null)
            {
                if (!CheckIfCardHasActiveBid(cardsInMarket[i]))
                {
                    cardManager.ReturnCardToDeck(cardsInMarket[i]);
                    cardsInMarket[i] = null;
                }
            }
        }
    }

    private bool CheckIfCardHasActiveBid(CriminalCard card)
    {
        foreach(MarketBid bid in currentBids)
        {
            if(bid.Card == card)
            {
                return true;
            }
        }
        return false;
    }
}
