using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeighborhoodType
{
    protected string neighborhoodName;
    public string NeighborhoodName { get { return neighborhoodName; } }
    protected int baseWealth;
    public int BaseWealth { get {  return baseWealth; } }
}

public class Slums : NeighborhoodType
{
    public Slums()
    {
        neighborhoodName = "Slums";
        baseWealth = 2;
    }
}

public class MerchantDistrict : NeighborhoodType
{
    public MerchantDistrict()
    {
        neighborhoodName = "Merchant District";
        baseWealth = 4;
    }
}
