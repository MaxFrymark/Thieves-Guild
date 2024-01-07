using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CriminalType
{
    protected string criminalName;
    public string CriminalName {  get { return criminalName; } }
    protected int strength;
    public int Strength { get { return strength; } }

    public enum CriminalTags { Fast, Stealth, Civilian }
    protected List<CriminalTags> tags = new List<CriminalTags>();
    public List<CriminalTags> Tags { get { return tags; } }
    protected string criminalDescription;
    public string CriminalDescription {  get { return criminalDescription; } }

    public abstract void OnPlay(Neighborhood neighborhood);
    public abstract void TakeAction(Neighborhood neighborhood);
}

public abstract class GuildMember : CriminalType
{

}

public abstract class Agent : CriminalType
{

}

public class Thief : GuildMember
{
    public Thief()
    {
        criminalName = "Thief";
        strength = 2;
        criminalDescription = "Steal 2 Coins Per Turn";
    }

    public override void OnPlay(Neighborhood neighborhood)
    {
        return;
    }

    public override void TakeAction(Neighborhood neighborhood)
    {
        return;
    }
}
