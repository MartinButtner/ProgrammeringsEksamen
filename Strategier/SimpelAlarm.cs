namespace Eksamenprogrammering3_2025.Strategier;

public class SimpelAlarm : IAlarmStrategi
{
    public string HandleAlarm(int distance)
    {
        if (distance <= 5)
        {
            return $"ALARM: Vandstanden er kritisk lav ({distance} cm)!";
        }
        else if (distance <= 25)
        {
            return $"ADVARSEL: Vandstanden er lav ({distance} cm).";
        }
        return string.Empty;
    }
}
