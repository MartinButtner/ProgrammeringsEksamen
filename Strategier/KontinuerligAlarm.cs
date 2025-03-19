namespace Eksamenprogrammering3_2025.Strategier;

public class KontinuerligAlarm : IAlarmStrategi
{
    private int _lastDistance = -1;
    private DateTime _lastAlertTime = DateTime.MinValue;

    public string HandleAlarm(int distance)
    {
        if (distance != _lastDistance || (DateTime.Now - _lastAlertTime).TotalMinutes >= 1)
        {
            _lastDistance = distance;
            _lastAlertTime = DateTime.Now;

            if (distance <= 5)
            {
                return $"ALARM: Vandstanden er kritisk lav ({distance} cm)!";
            }
            else if (distance <= 25)
            {
                return $"ADVARSEL: Vandstanden er lav ({distance} cm).";
            }
        }
        return string.Empty;
    }
}