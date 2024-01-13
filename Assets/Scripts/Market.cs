using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
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
        marketUI.SetBiddingDisplayActive(true);
        marketUI.SetUpDisplayCard(cardToBidOn);
        SetUpWorkingBid(cardToBidOn, biddingPlayer);
        marketUI.UpdateDisplayCardBidCounter(workingBid.CurrentBid);
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
                
            }

            else
            {
                SendCardToPlayer(currentBids[0]);
                currentBids.Remove(bidsToResolve[0]);
                bidsToResolve.RemoveAt(0);
            }
        }
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
}
