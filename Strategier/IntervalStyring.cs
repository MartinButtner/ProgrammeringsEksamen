namespace Eksamenprogrammering3_2025.Strategier;

public class IntervalStyring : IWaterBrakeStrategi
{
    private List<IntervalConfiguration> _intervals;

    public IntervalStyring(List<IntervalConfiguration> intervals)
    {
        _intervals = intervals;
    }

    public int CalculateOpenPercentage(int distance)
    {
        foreach (var interval in _intervals)
        {
            if (distance >= interval.MinDistance && distance <= interval.MaxDistance)
            {
                return interval.OpeningPercentage;
            }
        }

        return 0; // Default hvis ingen intervaller passer
    }

    public void UpdateConfiguration(List<IntervalConfiguration> newIntervals)
    {
        _intervals = newIntervals;
    }
}