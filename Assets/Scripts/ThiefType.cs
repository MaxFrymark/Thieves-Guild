using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThiefType
{
    protected string thiefName;
    public string ThiefName {  get { return thiefName; } }
    protected int strength;
    public int Strength { get { return strength; } }

    public abstract void TakeAction();
}
