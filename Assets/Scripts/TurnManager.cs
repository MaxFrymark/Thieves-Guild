using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Market market;
    [SerializeField] CardManager cardManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] City city;
    
    public void EndTurn()
    {
        AITakesAction();
        SelectTargets();
        FastActions();
        Battles();
        ResolveCriminalPlacements();
        ResolveMarketBids();
        DealNewMarket();
        AddCoinsToNeighborhoods();
        MoveGuard();
        StartNewTurn();
    }

    private void AITakesAction()
    {
        playerManager.AITakesAction();
    }

    private void SelectTargets()
    {

    }

    private void FastActions()
    {

    }

    private void Battles()
    {

    }

    private void ResolveCriminalPlacements()
    {
        playerManager.ClearPlayerActions();
        city.CriminalsAct();
    }

    private void ResolveMarketBids()
    {
        market.ResolveBids();
    }

    private void DealNewMarket()
    {
        market.ClearMarket();
        cardManager.SendCardsToMarket();
    }

    private void AddCoinsToNeighborhoods()
    {
        city.UpdateNeighborhoodCoins();
    }

    private void MoveGuard()
    {

    }

    private void StartNewTurn()
    {

    }
}
