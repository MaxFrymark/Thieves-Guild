using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRule
{
    private int numberOfTargets;
    public int NumberOfTargets { get {  return numberOfTargets; } }
    private string targetingDescription;
    public string TargetingDescription { get {  return targetingDescription; } }
    private CriminalCard owner;
    public CriminalCard Owner { get { return owner; } }

    public TargetRule(int numberOfTargets, string targetingDescription, CriminalCard owner)
    {
        this.numberOfTargets = numberOfTargets;
        this.targetingDescription = targetingDescription;
        this.owner = owner;
    }
}
