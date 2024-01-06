using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    List<Player> players = new List<Player>();
    
    void Start()
    {
        players.Add(new HumanPlayer());
        players.Add(new AIPlayer());
        SetUpPlayers();
    }

    private void SetUpPlayers()
    {
        foreach(Player player in players)
        {
            player.AddCoins(3);
        }
    }
}
