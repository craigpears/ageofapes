namespace BlazorApp1.Shared.FighterSimulator;

public class FighterConfiguration
{
    public Fighter Fighter { get; set; }
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
}