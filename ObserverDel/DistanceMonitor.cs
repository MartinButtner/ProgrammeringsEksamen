using System.Collections.Concurrent;
using Eksamenprogrammering3_2025.ProducerDel;

namespace Eksamenprogrammering3_2025.ObserverDel;

public class DistanceMonitor : Subject
{
    private readonly BlockingCollection<DistanceSample> _dataQueue;
    private bool _running = true;
    private DistanceSample _subjectState;

    public DistanceMonitor(BlockingCollection<DistanceSample> dataQueue)
    {
        _dataQueue = dataQueue;
    }

    public void Run()
    {
        while (_running || !_dataQueue.IsCompleted)
        {
            try
            {
                // Hent data fra BlockingCollection
                var sample = _dataQueue.Take();

                // Opdater SubjectState
                _subjectState = sample;

                // Notificer observers om ny data
                Notify();
            }
            catch (InvalidOperationException)
            {
                // Køen er tom og markeret som færdig
                break;
            }
        }
    }

    public void Stop()
    {
        _running = false;
    }

    public DistanceSample GetState()
    {
        return _subjectState;
    }
}