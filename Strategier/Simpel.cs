namespace Eksamenprogrammering3_2025.Strategier;

public class Simpel : IWaterBrakeStrategi
{
    public int CalculateOpenPercentage(int distance)
    {
        return distance >= 80 ? 0 : 100;
    }
}