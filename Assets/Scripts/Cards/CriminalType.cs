using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CriminalType
{
    protected string criminalName;
    public string CriminalName {  get { return criminalName; } }
    protected int strength;
    public int Strength { get { return strength; } }

    public enum CriminalTags { Fast, Stealth, Civilian, Vigilent, Pacifist }
    protected List<CriminalTags> tags = new List<CriminalTags>();
    public List<CriminalTags> Tags { get { return tags; } }
    protected string criminalDescription;
    public string CriminalDescription {  get { return criminalDescription; } }
    protected int influence;
    public int Influence { get { return influence; } }

    public abstract void SetTarget();
    public abstract void TakeAction();
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
        influence = 1;
        criminalDescription = "Steal 2 Coins Per Turn";
    }

    public override void SetTarget()
    {
        return;
    }

    public override void TakeAction()
    {
        return;
    }
}

public class Assassin : Agent
{
    public Assassin()
    {
        criminalName = "Assassin";
        strength = 3;
        criminalDescription = "Kill one enemy criminal in neighborhood.";
        tags.Add(CriminalTags.Fast);
        tags.Add(CriminalTags.Stealth);
    }

    public override void SetTarget() { return; }
    public override void TakeAction() { return; }
}
