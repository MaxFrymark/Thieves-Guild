using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCard : MonoBehaviour
{
    ThiefType thiefType;
    Player owner;

    public void AssignThiefType(ThiefType thiefType)
    {
        this.thiefType = thiefType;
    }

    public void AssignOwner(Player owner)
    {

    }
}
