namespace Eksamenprogrammering3_2025.ProducerDel;

public class DistanceSensor
{

    private readonly Random _random = new Random();
    private int _currentDistance = 80; // Initial distance
    public int SensorId { get; private set; }
    public bool Running { get; private set; } = true;

    public DistanceSensor(int sensorId)
    {
        SensorId = sensorId;
    }

    public (int Distance, DateTime Timestamp) GenerateSample()
    {
        // Generer et tilfældigt tal mellem 1 og 10
        int change = _random.Next(1, 11);

        // Justér afstand baseret på tilfældigt tal
        if (change < 6)
        {
            _currentDistance -= 2; // Vandstanden stiger
        }
        else
        {
            _currentDistance += 2; // Vandstanden falder
        }

        // Sikrer, at afstanden ikke bliver negativ
        if (_currentDistance < 0)
        {
            _currentDistance = 0;
        }

        // Returnér ny afstand og tidsstempel
        return (_currentDistance, DateTime.Now);
    }
    public void SetCurrentDistance(int distance) // Tilføjet for unittest
    {
        _currentDistance = distance;
    }
    public void Stop()
    {
        Running = false;
    }
}