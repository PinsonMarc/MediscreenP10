namespace MediscreenAPI.Model
{
    public class RiskRules
    {
        public string[] Triggers { get; set; } = null!;
        public int GapYear { get; set; }
        public RiskDefinition[] RiskLevels { get; set; } = null!;

        public string EvaluateRisk(int age, Sex sex, int nTriggers)
        {
            for (int i = RiskLevels.Length - 1; i >= 0; i--)
                if (RiskLevels[i].InsideRisk(age, sex, GapYear, nTriggers))
                    return RiskLevels[i].Name;
            return "None";
        }
    }

    public class RiskDefinition
    {
        public string Name { get; } = null!;
        public Rules? Rules { get; }

        public bool InsideRisk(int age, Sex sex, int gapYear, int nTriggers)
            => (Rules == null) ||
                (Rules.Over != null && age > gapYear && nTriggers >= Rules.Over) ||
                (sex == Sex.M && Rules.UnderM != null && age <= gapYear && nTriggers >= Rules.UnderM) ||
                (sex == Sex.F && Rules.UnderF != null && age <= gapYear && nTriggers >= Rules.UnderF);
    }

    public class Rules
    {
        public int? UnderM { get; }
        public int? UnderF { get; }
        public int? Over { get; }
    }
}