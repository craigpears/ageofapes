namespace BlazorApp1.Shared.FighterSimulator;

public class TalentSkill
{
    public string Name { get; set; }
    public Talent RootTalent { get; set; }
    public List<Boost> Boosts { get; set; }
    
}