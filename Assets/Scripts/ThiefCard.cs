using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCard : MonoBehaviour
{
    ThiefType thiefType;

    public void AssignThiefType(ThiefType thiefType)
    {
        this.thiefType = thiefType;
    }
}
