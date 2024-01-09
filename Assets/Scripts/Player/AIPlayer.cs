using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    Transform handLocation;

    public AIPlayer(Transform handLocation)
    {
        this.handLocation = handLocation;
    }

    public override void AddCriminalToDen(CriminalCard criminal)
    {
        if (den.Count < 5)
        {
            base.AddCriminalToDen(criminal);
            criminal.transform.position = handLocation.position;
        }
    }
}
