﻿namespace BlazorApp1.Shared.FighterSimulator;

public class UnitBoosts
{
    public double? AttackBoostPercent { get; set; }
    public double? DefenceBoostPercent { get; set; }
    public double? HealthBoostPercent { get; set; }
    public double? Damage { get; set; }
    public double? Counter { get; set; }
    public TroopType TroopType { get; set; }
}