namespace MediscreenAPI.Model
{
    public class RiskRules
    {
        public string[] Triggers { get; set; } = null!;
        public int GapYear { get; set; }
        public RiskDefinition[]? RiskLevels { get; set; }
    }

    public class RiskDefinition
    {
        public int? UnderM { get; set; }
        public int? UnderF { get; set; }
        public int? Over { get; set; }
    }
}