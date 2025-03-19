namespace Eksamenprogrammering3_2025.ObserverDel;

public abstract class Subject
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update();
        }
    }
}