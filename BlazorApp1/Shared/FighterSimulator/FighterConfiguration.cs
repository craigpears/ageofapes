namespace BlazorApp1.Shared.FighterSimulator;

public class FighterConfiguration
{
    public Fighter Fighter { get; set; }
    public List<Talent> SelectedTalents { get; set; }
    public ArmyBoosts ArmyBoosts { get; set; }
    public int TotalTalentPoints => SelectedTalents.Sum(x => x.TalentPointCost);

    public string TalentBreakdown => string.Join
        (" | ", 
            SelectedTalents
                .GroupBy(x => x.RootTalent.TalentTreeName)
                .Select(x => $"{x.Key} - {x.Sum(x => x.TalentPointCost)}").ToList()
        );

    public string FighterTalentBoosts => string.Join
        (" | ",
            SelectedTalents
                .SelectMany(x => x.Boosts)
                .GroupBy(x => x.BoostType)
                .Select(x => $"{x.Key} - {x.ToList().Sum(b => b.BoostAmounts.Max())}")
        );
}