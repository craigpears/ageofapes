namespace FightSimulator.Core.FighterSimulator;

public class Fighter
{
    protected FighterSkill _activeSkill;
    public string Name { get; set; }

    public List<TalentSkill> TalentSkills = new List<TalentSkill>();
    public List<FighterSkill> FighterSkills = new List<FighterSkill>();
    public bool CanTalentLeap { get; set; }

    public FighterSkill ActiveSkill
    {
        get
        {
            if (_activeSkill == null)
            {
                _activeSkill = FighterSkills.First(x => x.FighterSkillType == FigherSkillType.Active);
            }

            return _activeSkill;
        }
    }
    // TODO: Add fighter type to determine what relics/research can take effect
    // TODO: Add fighter equipment
    // TODO: Add some form of calculated fighter cost so they are comparable, like is spending on equipment better? relics? Talents?
}