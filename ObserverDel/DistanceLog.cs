using Eksamenprogrammering3_2025.Strategier;

namespace Eksamenprogrammering3_2025.ObserverDel;

public class DistanceLog : IObserver
{
    private readonly DistanceMonitor _monitor;
    private readonly IToFile _fileWriter;

    public DistanceLog(DistanceMonitor monitor, IToFile fileWriter)
    {
        _monitor = monitor;
        _fileWriter = fileWriter;
    }

    public void Update()
    {
        var sample = _monitor.GetState();
        Console.WriteLine($"{sample.Timestamp}: Distance: {sample.Distance}, Sensor: {sample.SensorId}");

        // Gem til fil
        _fileWriter.WriteToFile(sample);
    }
}


