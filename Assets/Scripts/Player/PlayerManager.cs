using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] HumanPlayerUI humanPlayerUI;
    
    [SerializeField] CardManager cardManager;
    [SerializeField] Transform aiPlayerHandLocation;

    List<Player> players = new List<Player>();
    
    void Start()
    {
        HumanPlayer humanPlayer = new HumanPlayer(Color.blue);
        humanPlayer.SetUpHumanPlayer(humanPlayerUI);
        players.Add(humanPlayer);
        FindAnyObjectByType<InputHandler>().AssignHumanPlayer(humanPlayer);
        players.Add(new AIPlayer(aiPlayerHandLocation, Color.red));
        SetUpPlayers();
    }

    private void SetUpPlayers()
    {
        foreach(Player player in players)
        {
            
            
            player.AddCoins(3);
            for(int i = 0; i < 2; i++)
            {
                Thief thief = new Thief();
                CriminalCard card = cardManager.CreateCard();
                card.AssignCriminalType(thief);
                player.AddCriminalToDen(card);
            }
        }
    }

    public void ClearPlayerActions()
    {
        foreach (Player player in players)
        {
            player.ClearPlays();
        }
    }

    public void AITakesAction()
    {
        foreach(Player player in players)
        {
            if(player is AIPlayer)
            {
                AIPlayer aiPlayer = (AIPlayer)player;
                aiPlayer.TakeAIAction();
            }
        }
    }
}
