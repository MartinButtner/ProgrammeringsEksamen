using Eksamenprogrammering3_2025.ProducerDel;

namespace Eksamenprogrammering3_2025.Strategier;

public interface IToFile
{
    void WriteToFile(DistanceSample sample);
    void WriteToFileString(string message);
}
  