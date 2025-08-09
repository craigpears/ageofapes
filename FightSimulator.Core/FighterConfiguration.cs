namespace FightSimulator.Core;

public class FighterConfiguration
{
    public Fighter Fighter { get; set; }
    public Fighter? DeputyFighter { get; set; }
    public int DeputySelectedTalent { get; set; }
    public List<Talent> SelectedTalents { get; set; }
    public ArmyBoosts ArmyBoosts { get; set; }

    public string TalentBreakdown => string.Join
        (" | ", 
            SelectedTalents
                .Where(x => x.TalentTreeName != null)
                .OrderBy(x => x.TalentTreeName)
                .GroupBy(x => x.TalentTreeName)
                .Select(x => $"{x.Key} - {x.Sum(x => x.TalentPointCost)}").ToList()
        );

    public TroopType PreferredTroopType
    {
        get
        {
            var troopTypeBoosts = SelectedTalents
                .SelectMany(x => x.Boosts)
                .Where(x => x.TroopRestriction != null)
                .Select(x => x.TroopRestriction)
                .ToList();

            return troopTypeBoosts.First() ?? TroopType.ThreeUnitTypes;
        }
    }

}