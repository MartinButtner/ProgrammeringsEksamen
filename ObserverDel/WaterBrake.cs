using Eksamenprogrammering3_2025.Strategier;

namespace Eksamenprogrammering3_2025.ObserverDel;

public class WaterBrake : IObserver
{
    private readonly DistanceMonitor _monitor;
    private IWaterBrakeStrategi _strategy; // Strategi-reference

    public WaterBrake(DistanceMonitor monitor, IWaterBrakeStrategi initialStrategy)
    {
        _monitor = monitor;
        _strategy = initialStrategy; // Sæt initial strategi
    }

    public void SetStrategy(IWaterBrakeStrategi strategy)
    {
        _strategy = strategy; // Skift strategi dynamisk
    }

    public void Update()
    {
        // Hent ny data fra DistanceMonitor
        var sample = _monitor.GetState();

        // Beregn åbningsgraden baseret på strategien
        int openingPercentage = _strategy.CalculateOpenPercentage(sample.Distance);

        // Udskriv status til konsollen
        Console.WriteLine($"WaterBrake åbning: {openingPercentage}%");
    }
}