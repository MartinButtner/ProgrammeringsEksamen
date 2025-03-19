using Eksamenprogrammering3_2025.ProducerDel;

namespace Eksamenprogrammering3_2025.Strategier;

public class ToFile : IToFile
{

    private readonly string _distanceFilePath;
    private readonly string _alarmFilePath;

    public ToFile(string distanceFilePath, string alarmFilePath)
    {
        _distanceFilePath = distanceFilePath;
        _alarmFilePath = alarmFilePath;
    }

    // Metode til at logge DistanceSample i sin egen fil
    public void WriteToFile(DistanceSample sample)
    {
        var logEntry = $"{sample.Timestamp}: Distance: {sample.Distance}, Sensor: {sample.SensorId}";
        File.AppendAllText(_distanceFilePath, logEntry + Environment.NewLine);
    }

    // Metode til at logge beskeder fra alarmer i en separat fil
    public void WriteToFileString(string message)
    {
        var logEntry = $"{DateTime.Now}: {message}";
        File.AppendAllText(_alarmFilePath, logEntry + Environment.NewLine);
    }
}