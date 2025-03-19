using Eksamenprogrammering3_2025.Strategier;

namespace Eksamenprogrammering3_2025.ObserverDel;

public class AlarmerSMS : IObserver
{
    private readonly DistanceMonitor _monitor;
    private IAlarmStrategi _alarmStrategi;
    private readonly IToFile _fileWriter;

    public AlarmerSMS(DistanceMonitor monitor, IAlarmStrategi alarmStrategi, IToFile fileWriter)
    {
        _monitor = monitor;
        _alarmStrategi = alarmStrategi;
        _fileWriter = fileWriter;
    }

    public void SetAlarmStrategi(IAlarmStrategi alarmStrategi)
    {
        _alarmStrategi = alarmStrategi;
    }

    public void Update()
    {
        var sample = _monitor.GetState();
        var message = _alarmStrategi.HandleAlarm(sample.Distance);

        if (!string.IsNullOrEmpty(message))
        {
            Console.WriteLine(message); // Skriver til konsollen
            _fileWriter.WriteToFileString(message); // Logger beskeden
        }
    }
}