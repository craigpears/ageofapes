﻿namespace BlazorApp1.Shared.FighterSimulator;

public enum BoostType
{
    IncreasedAttack,
    IncreasedHealth,
    IncreasedHealing,
    IncreasedMaxTroops,
    IncreasedDefence,
    IncreasedMarchingSpeed,
    ReducedDeadUnits,
    IncreasedDamage,
    IncreasedSkillDamage,
    IncreasedDamageToCounteredUnit, // e.g. pilots vs shooters increased damage
    IncreasedNormalAttackDamage,
    IncreasedCounterAttackDamage,
    TakesLessDamage,
    TakesLessDamageFromSentryTowers,
    IncreasedDamageToSentryTowers,
    TakesLessCounterAttackDamage,
    IncreasedRageOnReceiveNormalAttack,
    ReducedDamageFromNormalAttacks,
    EnemySkillDamageReduced
}