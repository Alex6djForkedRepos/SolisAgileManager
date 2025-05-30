namespace SolisManager.Shared.Models;

public class ConsumptionRequest
{
    public DateTime Start { get; set;  }
    public DateTime End { get; set; }

    public GroupByType GroupBy { get; set; } = GroupByType.Day;
}