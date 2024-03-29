using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    Market market;
    Transform handLocation;

    public AIPlayer(Transform handLocation, Color playerColor) : base(playerColor)
    {
        this.handLocation = handLocation;
        market = GameObject.FindAnyObjectByType<Market>();
    }

    public override void AddCriminalToDen(CriminalCard criminal)
    {
        if (den.Count < 5)
        {
            base.AddCriminalToDen(criminal);
            criminal.transform.position = handLocation.position;
        }
    }

    public void TakeAIAction()
    {
        if(coins > 2)
        {
            market.TakeBidFromAI(2, 2, this);
        }
        if(den.Count > 0)
        {
            if (den[0].CheckIfAICanPlay(City.Instance.Neighborhoods[0]))
            {
                City.Instance.TakeCardPlayFromAI(den[0], 0);
            }
        }
    }

    public override bool SpendCoin()
    {
        if (coins <= 0)
        {
            return false;
        }

        coins--;
        return true;
    }
}
