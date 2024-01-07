using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    HumanPlayerUI ui;

    public void SetUpHumanPlayer()
    {
        ui = new HumanPlayerUI();
        ui.SetUpUI();
    }

    public override void AddCoins(int additionalCoins)
    {
        base.AddCoins(additionalCoins);
        ui.UpdateCoinCounter(coins);
    }

    public override void AddCriminalToDen(CriminalCard criminal)
    {
        if (ui.AddCardToHand(criminal.gameObject))
        {
            base.AddCriminalToDen(criminal);
        }
    }
}
