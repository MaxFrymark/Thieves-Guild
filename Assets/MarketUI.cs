using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketUI : MonoBehaviour
{
    [SerializeField] GameObject biddingDisplay;
    [SerializeField] DisplayCard displayCard;
    [SerializeField] TextMeshProUGUI displayCardBidCounter;
    [SerializeField] TextMeshProUGUI[] cardBidCounters;

    public void SetBiddingDisplayActive(bool isBiddingDisplayActive)
    {
        biddingDisplay.SetActive(isBiddingDisplayActive);
    }

    public void SetUpDisplayCard(CriminalCard criminalCard)
    {
        displayCard.SetUpCardDisplay(criminalCard);
    }

    public void UpdateBidCounters(int index, int counter)
    {
        cardBidCounters[index].text = counter.ToString();
    }

    public void UpdateDisplayCardBidCounter(int counter)
    {
        displayCardBidCounter.text = counter.ToString();
    }
}
