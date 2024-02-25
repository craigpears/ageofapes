﻿namespace BlazorApp1.Shared.FighterSimulator.Boosts;

public class RelicBoostsRepository
{
    public List<Boost> GetBoosts()
    {
        var relicBoosts = new List<Boost>();

        // We're more interested in the potential of each unit and fighter, so just use two values for when it is on/off display
        
        
        #region common

        var spaceHelmet = new List<Boost>
        {
            new Boost
            {
                BoostType = BoostType.NeutralUnitBreaker, // TODO: This needs researching and implementing
                BoostAmounts = new List<double> { 0, 4.0 }
            },
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 12.0 },
                TroopRestriction = TroopType.Shooter
            }
        };
        
        relicBoosts.AddRange(spaceHelmet);

        var wasabiCan = new List<Boost>
        {
            new Boost
            {
                BoostType = BoostType.IncreasedHealth,
                BoostAmounts = new List<double> { 2.0, 2.0 },
                TroopRestriction = TroopType.Shooter
            },
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 12.0 },
                TroopRestriction = TroopType.Shooter
            }
        };
        
        relicBoosts.AddRange(wasabiCan);

        var magicBox = new List<Boost>
        {
            new Boost
            {
                BoostType = BoostType.IncreasedHealth,
                BoostAmounts = new List<double> { 2.0, 2.0 },
                TroopRestriction = TroopType.Hitter
            },
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 12.0 },
                TroopRestriction = TroopType.Hitter
            }
        };
        
        relicBoosts.AddRange(magicBox);

        var metalDrum = new List<Boost>
        {
            new Boost
            {
                BoostType = BoostType.IncreasedHealth,
                BoostAmounts = new List<double> { 2.0, 2.0 },
                TroopRestriction = TroopType.Pilot
            },
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 6.0 },
            }
            ,
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 12.0 },
                TroopRestriction = TroopType.Pilot
            }
        };
        
        relicBoosts.AddRange(metalDrum);
        
        var alarmBell = new Boost
        {
            BoostType = BoostType.IncreasedHealth,
            BoostAmounts = new List<double> { 0.0, 1.6 },
            TroopRestriction = TroopType.Pilot
        };
        
        relicBoosts.Add(alarmBell);
        
        var woodenCup = new Boost
        {
            BoostType = BoostType.IncreasedHealth,
            BoostAmounts = new List<double> { 0.0, 1.6 },
            TroopRestriction = TroopType.Shooter
        };
        
        relicBoosts.Add(woodenCup);
        
        var woodenShield = new List<Boost> {
            new Boost
            {
                BoostType = BoostType.IncreasedDamage,
                BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits,
                BoostAmounts = new List<double> { 10.0, 10.0 }
            },
            new Boost
            {
                BoostType = BoostType.IncreasedHealth,
                BoostAmounts = new List<double> { 0.0, 1.6 },
                TroopRestriction = TroopType.WallBreaker
            }
        };
        
        relicBoosts.AddRange(woodenShield);
        
        var bloodletterAxe = new List<Boost> {
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 6.0, 9.6 },
                TroopRestriction = TroopType.Pilot
            }
        };
        
        relicBoosts.AddRange(bloodletterAxe);
        
        var dentalPicker = new List<Boost> {
            new Boost
            {
                BoostType = BoostType.IncreasedSkillDamage,
                BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits,
                BoostAmounts = new List<double> { 20.0, 20.0 },
                TroopRestriction = TroopType.Pilot
            }
        };
        
        relicBoosts.AddRange(dentalPicker);
        
        var orthopedicHammer = new List<Boost> {
            new Boost
            {
                BoostType = BoostType.IncreasedAttack,
                BoostAmounts = new List<double> { 2.0, 3.2 },
                TroopRestriction = TroopType.Pilot
            }
        };
        
        relicBoosts.AddRange(dentalPicker);
        
        var streetBlocker = new List<Boost> {
            new Boost
            {
                BoostType = BoostType.IncreasedMarchingSpeed,
                BoostAmounts = new List<double> { 0.0, 4.8 },
                TroopRestriction = TroopType.Hitter
            }
        };
        
        relicBoosts.AddRange(streetBlocker);
        
        // TODO: Finish adding relics
        
        #endregion
        

        relicBoosts.ForEach(x => x.Source = "Relics");
        return relicBoosts;
    }
}