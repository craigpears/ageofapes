namespace BlazorApp1.Shared.FighterSimulator;

public class Fighter
{
    public string Name { get; set; }

    public List<TalentSkill> TalentSkills = new List<TalentSkill>();
    public List<FighterSkill> FighterSkills = new List<FighterSkill>();
    public bool CanTalentLeap { get; set; }
    // TODO: Add fighter type to determine what relics/research can take effect
    // TODO: Add fighter equipment
    // TODO: Add some form of calculated fighter cost so they are comparable, like is spending on equipment better? relics? Talents?
}