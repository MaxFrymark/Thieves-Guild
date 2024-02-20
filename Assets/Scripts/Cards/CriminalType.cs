using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;

public abstract class CriminalType
{
    protected CriminalCard attachedCard;
    
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
    private Sprite imageSprite;
    public Sprite ImageSprite { get { return imageSprite; } }
    protected string assetReference;

    protected TargetRule targetRule;
    public TargetRule TargetRule { get { return targetRule; } }

    public CriminalType(CriminalCard attachedCard)
    {
        this.attachedCard = attachedCard;
    }

    public abstract bool CheckIfAICanPlay(Neighborhood neighborhood);

    public void GetTarget()
    {
        if(attachedCard.Owner is HumanPlayer)
        {
            GetTargetForHuman();
        }
        else
        {
            GetTargetForAI();
        }
    }
    protected abstract void GetTargetForHuman();
    protected abstract void GetTargetForAI();

    public abstract void SetTarget<T>(T target);
    public abstract void TakeAction();

    protected void LoadSpriteFromAddressables(string assetAddress)
    {
        Addressables.LoadAssetAsync<Sprite>(assetAddress).Completed += OnLoadDone;
    }

    protected void OnLoadDone(AsyncOperationHandle<Sprite> sprite)
    {
        imageSprite = sprite.Result;
        attachedCard.SetCardImage(imageSprite);
    }
}

public abstract class GuildMember : CriminalType
{
    public GuildMember(CriminalCard attachedCard) : base(attachedCard) { }
}

public abstract class Agent : CriminalType
{
    public Agent(CriminalCard attachedCard) : base (attachedCard) { }
}

public class Thief : GuildMember
{
    public Thief(CriminalCard attachedCard) : base(attachedCard)
    {
        criminalName = "Thief";
        strength = 2;
        influence = 1;
        criminalDescription = "Steal 2 Coins Per Turn";
        assetReference = "ThiefCardArt";
        LoadSpriteFromAddressables(assetReference);
    }
    
    public override bool CheckIfAICanPlay(Neighborhood neighborhood)
    {
        return true;
    }

    protected override void GetTargetForHuman()
    {
        return;
    }

    protected override void GetTargetForAI()
    {
        return;
    }

    public override void SetTarget<T>(T target)
    {
        return;
    }

    public override void TakeAction()
    {
        attachedCard.CurrentNeighborhood.StealFromNeighborhood(2, attachedCard);
    }
}

public class Assassin : Agent
{
    CriminalCard assassinationTarget;

    public Assassin(CriminalCard attachedCard) : base(attachedCard)
    {
        criminalName = "Assassin";
        strength = 3;
        criminalDescription = "Kill one enemy criminal in neighborhood.";
        tags.Add(CriminalTags.Fast);
        tags.Add(CriminalTags.Stealth);
        assetReference = "AssassinCardArt";
        targetRule = new TargetRule(1, "Select One Target To Kill", attachedCard);
        LoadSpriteFromAddressables(assetReference);
    }

    public override bool CheckIfAICanPlay(Neighborhood neighborhood)
    {
        foreach(CriminalCard card in neighborhood.CriminalsInNeighborHood)
        {
            if(card.Owner != attachedCard.Owner)
            {
                return true;
            }
        }
        Debug.Log(criminalName);
        return false;
    }

    protected override void GetTargetForHuman()
    {
        List<CriminalCard> possibleTargets = new List<CriminalCard> ();
        foreach(CriminalCard card in attachedCard.CurrentNeighborhood.CriminalsInNeighborHood)
        {
            if(card.Owner != attachedCard.Owner)
            {
                possibleTargets.Add(card);
            }
        }
        
        HumanPlayer owner = attachedCard.Owner as HumanPlayer;
        owner.OpenTargeting(possibleTargets, attachedCard);
    }

    protected override void GetTargetForAI()
    {
        List<CriminalCard> possibleTargets = new List<CriminalCard>();
        foreach(CriminalCard card in attachedCard.CurrentNeighborhood.CriminalsInNeighborHood)
        {
            if(card.Owner != attachedCard.Owner)
            {
                possibleTargets.Add(card);
            }
        }

        if(possibleTargets.Count > 0)
        {
            SetTarget(possibleTargets);
        }

        else
        {
            Debug.Log("Invalid AI Play: No Targets");
        }
    }

    public override void SetTarget<T>(T target)
    {
        if(target is List<CriminalCard>)
        {
            List<CriminalCard> targetList = target as List<CriminalCard>;
            assassinationTarget = targetList[0];
        }
    }
    public override void TakeAction()
    {
        assassinationTarget.SendToGraveyard();
    }
}
