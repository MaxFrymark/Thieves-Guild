using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardTracker : MonoBehaviour
{
    private List<Neighborhood> neighborhoodsWithCardOverlapping = new List<Neighborhood>();
    private CardControl cardTracking = null;
    private Neighborhood closestNeighborhoodToTrackedCard = null;

    void Update()
    {
        if (cardTracking != null)
        {
            if (neighborhoodsWithCardOverlapping.Count == 0)
            {
                cardTracking = null;
                closestNeighborhoodToTrackedCard = null;
            }
            else
            {
                FindClosestNeighborhood();
            }
            City.Instance.HighlightNeighborhoodToPlayOn(closestNeighborhoodToTrackedCard);
        }
    }

    public void AddNeighborhoodToTrackingList(CardControl card, Neighborhood neighborhood)
    {
        if (cardTracking == null)
        {
            cardTracking = card;
        }
        neighborhoodsWithCardOverlapping.Add(neighborhood);
    }

    public void RemoveNeighborhoodFromTrackingList(Neighborhood neighborhood)
    {
        neighborhoodsWithCardOverlapping.Remove(neighborhood);
    }

    private void FindClosestNeighborhood()
    {
        Neighborhood tempNeighborhood = null;

        foreach (Neighborhood neighborhood in neighborhoodsWithCardOverlapping)
        {
            if (tempNeighborhood == null)
            {
                tempNeighborhood = neighborhood;
            }

            else
            {
                if (Vector2.Distance(transform.position, tempNeighborhood.transform.position) > Vector2.Distance(transform.position, neighborhood.transform.position))
                {
                    tempNeighborhood = neighborhood;
                }
            }
        }
        
        closestNeighborhoodToTrackedCard = tempNeighborhood;
    }

    public Neighborhood GetClosestNeighborhoodToTrackedCard()
    {
        return closestNeighborhoodToTrackedCard;
    }
}
