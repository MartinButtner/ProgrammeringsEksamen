using System.Collections.Concurrent;

namespace Eksamenprogrammering3_2025.ProducerDel;

public class DistanceSensorReader
{
    private readonly BlockingCollection<DistanceSample> _dataQueue;
    private readonly DistanceSensor _sensor1;
    private readonly DistanceSensor _sensor2;
    private readonly DistanceSensor _sensor3;
    private bool _running = true;

    public DistanceSensorReader(BlockingCollection<DistanceSample> dataQueue, DistanceSensor sensor1, DistanceSensor sensor2, DistanceSensor sensor3)
    {
        _dataQueue = dataQueue;
        _sensor1 = sensor1;
        _sensor2 = sensor2;
        _sensor3 = sensor3;
    }


    public void Run()
    {
        int simulatedTimeOffset = 0;

        while (_running)
        {
            var sample1 = _sensor1.GenerateSample();
            var sample2 = _sensor2.GenerateSample();
            var sample3 = _sensor3.GenerateSample();

            // Filtrer og beregn gennemsnittet af de nærmeste værdier
            var distances = new List<int> { sample1.Distance, sample2.Distance, sample3.Distance };
            distances.Sort();
            int averageDistance = (distances[0] + distances[1]) / 2;

            // Opret en ny DistanceSample med det simulerede tidsstempel
            var sample = new DistanceSample
            {
                Distance = averageDistance,
                Timestamp = DateTime.Now.AddSeconds(simulatedTimeOffset), // Tilføj offset
                SensorId = 1 // Samlet måling
            };

            // Tilføj instansen til BlockingCollection
            _dataQueue.Add(sample);

            // Øg offset for at simulere 10 sekunder mellem målinger
            simulatedTimeOffset += 9;

            // Simuler et reelt interval på 1 sekund mellem iterationer
            Thread.Sleep(1000);
        }

        _dataQueue.CompleteAdding();
    }

    public void Stop()
    {
        _running = false;
    }
}
