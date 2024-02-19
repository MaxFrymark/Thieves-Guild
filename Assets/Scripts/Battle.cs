using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    private Neighborhood battleField;
    List<List<CriminalCard>> armiesInBattle = new List<List<CriminalCard>>();

    public Battle(Neighborhood battleField)
    {
        this.battleField = battleField;
        CreateArmies();
    }

    private void CreateArmies()
    {
        List<Player> playersWithArmy = new List<Player>();
        foreach(CriminalCard criminal in battleField.CriminalsInNeighborHood)
        {
            if (!playersWithArmy.Contains(criminal.Owner))
            {
                playersWithArmy.Add(criminal.Owner);
            }
        }

        foreach(Player player in playersWithArmy)
        {
            armiesInBattle.Add(new List<CriminalCard>());
        }

        foreach(CriminalCard criminal in battleField.CriminalsInNeighborHood)
        {
            for(int i = 0; i < playersWithArmy.Count; i++)
            {
                if (playersWithArmy[i] == criminal.Owner)
                {
                    armiesInBattle[i].Add(criminal);
                }
            }
        }
        CalculateStrengths();
    }

    private void CalculateStrengths()
    {
        List<int> armyStrengths = new List<int>();
        int armyIndex = 0;
        foreach(List<CriminalCard> army in armiesInBattle)
        {
            armyStrengths.Add(0);
            foreach(CriminalCard criminal in army)
            {
                armyStrengths[armyIndex] += criminal.CriminalType.Strength;
            }
            armyIndex++;
        }
        CalculateWinner(armyStrengths);
    }

    private void CalculateWinner(List<int> armyStrengths)
    {
        int highestStrength = 0;
        foreach(int strength in armyStrengths)
        {
            if(strength > highestStrength)
            {
                highestStrength = strength;
            }
        }
        List<List<CriminalCard>> armiesWithHighestStrength = new List<List<CriminalCard>>();
        for(int i = 0; i < armyStrengths.Count; i++)
        {
            if (armyStrengths[i] == highestStrength)
            {
                armiesWithHighestStrength.Add(armiesInBattle[i]);
            }
        }
        foreach(List<CriminalCard> army in armiesInBattle)
        {
            if (!armiesWithHighestStrength.Contains(army))
            {
                ArmyLoses(army);
            }
        }

        if(armiesWithHighestStrength.Count > 1)
        {
            Draw(armiesWithHighestStrength);
        }
        else
        {
            ArmyWins(armiesWithHighestStrength[0]);
        }
    }

    private void ArmyLoses(List<CriminalCard> losingArmy)
    {
        CriminalCard killedCriminal = losingArmy[Random.Range(0, losingArmy.Count)];
        killedCriminal.SendToGraveyard();
        losingArmy.Remove(killedCriminal);

        foreach(CriminalCard criminal in losingArmy)
        {
            criminal.SendToDen(criminal.Owner);
        }
    }

    private void Draw(List<List<CriminalCard>> armiesInDraw)
    {

    }

    private void ArmyWins(List<CriminalCard> winningArmy)
    {

    }
}
