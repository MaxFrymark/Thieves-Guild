using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    HumanPlayerUI ui;

    public HumanPlayer(Color playerColor) : base(playerColor)
    {

    }

    public void SetUpHumanPlayer(HumanPlayerUI humanPlayerUI)
    {
        ui = humanPlayerUI;
        ui.SetUpUI(this);
    }

    public override void AddCoins(int additionalCoins)
    {
        base.AddCoins(additionalCoins);
        ui.UpdateCoinCounter(coins);
    }

    public override void AddCriminalToDen(CriminalCard criminal)
    {
        if (ui.AddCardToHand(criminal.CardControl))
        {
            base.AddCriminalToDen(criminal);
        }
    }

    public override void RemoveCriminalFromDen(CriminalCard criminal)
    {
        ui.RemoveCardFromHand(criminal.CardControl);
        base.RemoveCriminalFromDen(criminal);
    }

    public override bool SpendCoin()
    {
        if(coins <= 0)
        {
            return false;
        }

        coins--;
        ui.UpdateCoinCounter(coins);
        return true;
    }

    public void CancelPlay()
    {
        if (cardPlays.Count > 0)
        {
            CardPlay canceledPlay = cardPlays[cardPlays.Count - 1];
            cardPlays.Remove(canceledPlay);
            canceledPlay.Card.SendToDen(canceledPlay.Card.Owner);
        }
    }
}
