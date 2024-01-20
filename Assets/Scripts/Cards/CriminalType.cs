using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

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

    
    public abstract void SetTarget();
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

    public void SetAttachedCard(CriminalCard card)
    {
        attachedCard = card;
    }
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
        assetReference = "ThiefCardArt";
        LoadSpriteFromAddressables(assetReference);
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
        assetReference = "AssassinCardArt";
        LoadSpriteFromAddressables(assetReference);
    }

    public override void SetTarget() { return; }
    public override void TakeAction() { return; }
}
