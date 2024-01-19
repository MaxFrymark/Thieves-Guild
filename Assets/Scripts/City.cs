using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    private static City instance;
    public static City Instance { get { return instance; } }
    
    [SerializeField] CardTracker cardTracker;
    
    Neighborhood[] neighborhoods;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        neighborhoods = FindObjectsByType<Neighborhood>(FindObjectsSortMode.InstanceID);
        neighborhoods[0].AssignNeighborhoodType(this, new Slums());
        neighborhoods[1].AssignNeighborhoodType(this, new MerchantDistrict());
    }

    public void UpdateNeighborhoodCoins()
    {
        foreach (Neighborhood neighborhood in neighborhoods)
        {
            neighborhood.UpdateCoinsForNewTurn();
        }
    }

    public Neighborhood GetNeighborhoodFromTracker()
    {
        return cardTracker.GetClosestNeighborhoodToTrackedCard();
    }

    public void HighlightNeighborhoodToPlayOn(Neighborhood neighborhoodToPlay)
    {
        foreach(Neighborhood neighborhood in neighborhoods)
        {
            if(neighborhood == neighborhoodToPlay)
            {
                neighborhood.HighlightNeighborHood();
            }
            else
            {
                neighborhood.RemoveHighlightNeighborhood();
            }
        }
    }

    public void AddNeighborhoodToTrackingList(CardControl card, Neighborhood neighborhood) => cardTracker.AddNeighborhoodToTrackingList(card, neighborhood);
    public void RemoveNeighborhoodFromTrackingList(Neighborhood neighborhood) => cardTracker.RemoveNeighborhoodFromTrackingList(neighborhood);

}
