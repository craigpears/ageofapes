namespace BlazorApp1.Shared.FighterSimulator;

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
    FiveSecondsAfterActiveSkillRelease,
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
}