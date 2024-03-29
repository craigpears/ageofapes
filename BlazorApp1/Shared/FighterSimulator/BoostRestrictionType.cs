﻿namespace BlazorApp1.Shared.FighterSimulator;

public enum BoostRestrictionType
{
    AttackingCitiesOnly,
    SeigeMode,
    LeadingRally,
    GatheringResources,
    AttackingGatherers,
    Garrison,
    MapBattle,
    DefendingAgainstMultipleTroops,
    TwoSecondsAfterActiveSkillRelease,
    ThreeSecondsAfterHitByActiveSkill,
    ThreeSecondsAfterActiveSkillRelease,
    FiveSecondsAfterActiveSkillRelease,  // TODO: Use duration property for these variations
    FirstFiveSecondsOfBattle,
    TenSecondsAfterLeavingCity,
    ThreeSecondsAfterHealing,
    ThreeSecondsAfterSuccessfulChance,
    HealthBelowHalf,
    HealthAbove90,
    HealthAbove80,
    HealthBelow80,
    HealthAbove70,
    AttackingNeutralUnits,
    MultipliedByAdjacentAllies,
    TwoSecondsAfterTakingSkillDamage,
    AfterTakingSkillDamage,
    FirstTenSecondsOfBattle,
    TroopNumberGreaterThanEnemy,
    AfterNormalAttack,
    AfterActiveSkillRelease
}