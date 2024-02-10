namespace BlazorApp1.Shared.FighterSimulator;

public class FighterConfiguration
{
    public Fighter Fighter { get; set; }
    public List<Talent> SelectedTalents { get; set; }
    public bool RequiresTalentLeap { get; set; }
    public ArmyBoosts ArmyBoosts { get; set; }
    public int TalentPointCount => SelectedTalents.Count;
}