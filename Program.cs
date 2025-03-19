using System.Collections.Concurrent;
using Eksamenprogrammering3_2025.ProducerDel;
using Eksamenprogrammering3_2025.ObserverDel;
using Eksamenprogrammering3_2025.Strategier;
using System.Security.Claims;

namespace Eksamenprogrammering3_2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configurer = new Configurer("config.json");

            // Eksempel på standardkonfiguration
            var defaultConfig = new List<IntervalConfiguration>
    {
        new IntervalConfiguration { MinDistance = 80, MaxDistance = int.MaxValue, OpeningPercentage = 0 },
        new IntervalConfiguration { MinDistance = 61, MaxDistance = 79, OpeningPercentage = 100 },
        new IntervalConfiguration { MinDistance = 41, MaxDistance = 60, OpeningPercentage = 80 },
        new IntervalConfiguration { MinDistance = 21, MaxDistance = 40, OpeningPercentage = 50 },
        new IntervalConfiguration { MinDistance = 0, MaxDistance = 20, OpeningPercentage = 15 }
    };

            // Gem standardkonfigurationen, hvis filen ikke findes
            if (!System.IO.File.Exists("config.json"))
            {
                configurer.SaveConfiguration(defaultConfig);
            }

            // Læs konfigurationen fra filen
            var intervals = configurer.LoadConfiguration();

            //
            var fileWriter = new ToFile("DistanceLog2.txt", "alarm_messages.txt");

            // Initialiser IntervalStyring med konfigurationen
            var intervalStyring = new IntervalStyring(intervals);
            var simpelStyring = new Simpel(); // For simple styring

            var dataQueue = new BlockingCollection<DistanceSample>();
            // 3 forskellige sensore
            var sensor1 = new DistanceSensor(1);
            var sensor2 = new DistanceSensor(2);
            var sensor3 = new DistanceSensor(3);

            var monitor = new DistanceMonitor(dataQueue);
            var log = new DistanceLog(monitor,fileWriter);
            var waterBrake = new WaterBrake(monitor, intervalStyring); // Start med intervalStyring
            var alarmerSMS = new AlarmerSMS(monitor,new SimpelAlarm(),fileWriter);

            monitor.Attach(log);
            monitor.Attach(waterBrake);
            monitor.Attach(alarmerSMS);

            var sensorReader = new DistanceSensorReader(dataQueue, sensor1,sensor2,sensor3);

            var monitorThread = new Thread(monitor.Run);
            var sensorThread = new Thread(sensorReader.Run);

            monitorThread.Start();
            sensorThread.Start();

            Console.WriteLine(
                "Tryk på følgende taster for at vælge en funktion:\n" +
                "'q' - Stop programmet\n" +
                "'i' - Skift til IntervalStyring\n" +
                "'s' - Skift til SimpelStyring\n" +
                "'m' - Skift til SimpelAlarm\n" +
                "'k' - Skift til KontinuerligAlarm\n" +
                "'u' - Opdater konfiguration"
            );

            bool running = true;
            while (running)
            {
                var input = Console.ReadKey(intercept: true).KeyChar;

                switch (input)
                {
                    case 'q':
                        running = false;
                        sensorReader.Stop();
                        monitor.Stop();
                        Console.WriteLine("\nSystemet stoppes...");
                        break;

                    case 'i':
                        waterBrake.SetStrategy(intervalStyring);
                        Console.WriteLine("\nSkiftet til IntervalStyring.");
                        break;

                    case 's':
                        waterBrake.SetStrategy(simpelStyring);
                        Console.WriteLine("\nSkiftet til SimpelStyring.");
                        break;

                    case 'm':
                        alarmerSMS.SetAlarmStrategi(new SimpelAlarm());
                        Console.WriteLine("\nSkiftet til SimpelAlarm.");
                        break;

                    case 'k':
                        alarmerSMS.SetAlarmStrategi(new KontinuerligAlarm());
                        Console.WriteLine("\nSkiftet til KontinuerligAlarm.");
                        break;

                    case 'u':
                        var updatedConfig = configurer.LoadConfiguration();
                        intervalStyring.UpdateConfiguration(updatedConfig);
                        Console.WriteLine("\nKonfiguration opdateret fra fil.");
                        break;

                    default:
                        Console.WriteLine("\nUgyldigt input. Tryk 'q', 'i', 's', 'm', 'k' eller 'u'.");
                        break;
                }
            }

            monitorThread.Join();
            sensorThread.Join();

            Console.WriteLine("Programmet er afsluttet.");
        }
    }
    }
    

