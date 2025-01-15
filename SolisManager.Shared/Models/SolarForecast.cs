namespace SolisManager.Shared.Models;

/// <summary>
/// A 30-minute estimate of the amount of solar generation in kWh
/// </summary>
public class SolarForecast
{
    public decimal ForecastkWh { get; set; }
    public DateTime PeriodStart { get; set; }
    
    public override string ToString()
    {
        return $"{PeriodStart} - {ForecastkWh}kWh";
    }

}