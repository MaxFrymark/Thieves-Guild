using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    [SerializeField] Transform aiPlayerHandLocation;

    List<Player> players = new List<Player>();
    
    void Start()
    {
        HumanPlayer humanPlayer = new HumanPlayer();
        humanPlayer.SetUpHumanPlayer();
        players.Add(humanPlayer);
        players.Add(new AIPlayer(aiPlayerHandLocation));
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
}
