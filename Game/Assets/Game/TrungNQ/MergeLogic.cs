
public class MergeLogic
{
    public Trung_DemoHeroCombatAttribute Merge(Trung_DemoHeroCombatAttribute att1, Trung_DemoHeroCombatAttribute att2)
    {
        if (CheckMerge(att1, att2))
            return new Trung_DemoHeroCombatAttribute()
            {
                heroClass = att1.heroClass,
                heroStar = att2.heroStar + 1
            };
        return null;
    }

    public bool CheckMerge(Trung_DemoHeroCombatAttribute att1, Trung_DemoHeroCombatAttribute att2)
    {
        if (att1.heroStar >= 7 || att2.heroStar >= 7) return false;
        return att1.heroClass == att2.heroClass && att1.heroStar == att2.heroStar;
    }
}

public interface IMergeableHero
{
    public Trung_DemoHeroCombatAttribute GetAttribute();
    public void SetAttribute(Trung_DemoHeroCombatAttribute newAtt);
    public void Merge(IMergeableHero other, MergeLogic logic);

}