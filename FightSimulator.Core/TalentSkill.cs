namespace FightSimulator.Core;

public class TalentSkill
{
    public string Name { get; set; }
    public Talent TalentTree { get; set; }
    public List<Boost> Boosts { get; set; }
    
}