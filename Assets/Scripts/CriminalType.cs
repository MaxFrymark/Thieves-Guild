using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CriminalType
{
    protected string thiefName;
    public string ThiefName {  get { return thiefName; } }
    protected int strength;
    public int Strength { get { return strength; } }

    public enum CriminalTags { Fast, Stealth, Civilian }
    protected List<CriminalTags> tags = new List<CriminalTags>();

    public abstract void OnPlay(Neighborhood neighborhood);
    public abstract void TakeAction(Neighborhood neighborhood);
}

public abstract class GuildMember : CriminalType
{

}

public abstract class Agent : CriminalType
{

}
