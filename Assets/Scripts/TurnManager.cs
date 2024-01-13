using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Market market;
    
    public void EndTurn()
    {
        FastActions();
        Battles();
        ResolveCriminalPlacements();
        ResolveMarketBids();
        DealNewMarket();
        AddCoinsToNeighborhoods();
        MoveGuard();
        StartNewTurn();
    }

    private void FastActions()
    {

    }

    private void Battles()
    {

    }

    private void ResolveCriminalPlacements()
    {

    }

    private void ResolveMarketBids()
    {
        market.ResolveBids();
    }

    private void DealNewMarket()
    {

    }

    private void AddCoinsToNeighborhoods()
    {

    }

    private void MoveGuard()
    {

    }

    private void StartNewTurn()
    {

    }
}
