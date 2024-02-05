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

    public void CriminalsAct()
    {
        foreach(Neighborhood neighborhood in neighborhoods)
        {
            neighborhood.CriminalsAct();
        }
    }

    public void CancelPlay(CriminalCard card)
    {
        foreach(Neighborhood neighborhood in neighborhoods)
        {
            if(card.CurrentNeighborhood == neighborhood)
            {
                neighborhood.RemoveCriminalFromNeighborhood(card);
            }
        }
    }

    public void TakeCardPlayFromAI(CriminalCard card, int index)
    {
        card.PlayCardToNeighborhood(neighborhoods[index]);
    }

    public void CheckCityForBattles()
    {
        foreach(Neighborhood neighborhood in neighborhoods)
        {
            CheckNeighborhoodForBattle(neighborhood);
        }
    }

    private void CheckNeighborhoodForBattle(Neighborhood neighborhood)
    {
        List<Player> listOfPlayersWithCriminalsInNeighborhood = new List<Player> ();
        foreach(CriminalCard criminal in neighborhood.CriminalsInNeighborHood)
        {
            if (!criminal.CriminalType.Tags.Contains(CriminalType.CriminalTags.Stealth))
            {
                if (!listOfPlayersWithCriminalsInNeighborhood.Contains(criminal.Owner))
                {
                    listOfPlayersWithCriminalsInNeighborhood.Add(criminal.Owner);
                }
            }
        }

        if(listOfPlayersWithCriminalsInNeighborhood.Count > 1)
        {
            new Battle(neighborhood);
        }
    }

    public void AddNeighborhoodToTrackingList(CardControl card, Neighborhood neighborhood) => cardTracker.AddNeighborhoodToTrackingList(card, neighborhood);
    public void RemoveNeighborhoodFromTrackingList(Neighborhood neighborhood) => cardTracker.RemoveNeighborhoodFromTrackingList(neighborhood);
    public void StopTrackingCard() => cardTracker.StopTrackingCard();
}
