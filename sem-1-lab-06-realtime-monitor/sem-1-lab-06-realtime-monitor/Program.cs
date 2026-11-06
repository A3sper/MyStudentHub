using sem_1_lab_06_realtime_monitor;
using System;

public class Program
{
    public static List<double> ram = new List<double>();
    public static List<double> cpu = new List<double>();
    public static List<double> temperature = new List<double>();

    private static void Main()
    {
        // Создаём объект, который будет получать данные о состоянии сервера.
        var monitor = new ServerMonitor();

        // Сообщаем библиотеке, какой метод нужно вызывать
        // каждый раз, когда появляются новые данные.
        monitor.DataReceived += OnDataReceived;

        // Запускаем получение данных.
        // После этого библиотека сама будет передавать новые значения примерно раз в секунду.
        monitor.Start();

        Console.WriteLine("Мониторинг запущен.");
        Console.WriteLine("Для завершения программы нажмите Enter.");

        // Не даём программе завершиться сразу после запуска.
        Console.ReadLine();

        // При желании можно остановить получение данных.
        monitor.Stop();
    }
    public static void SaveData(double cpu_get, double ram_get, double temperature_get)
    {
        cpu.Add(cpu_get);
        if (cpu.Count > 20)
        {
            cpu.RemoveAt(0);
        }
        ram.Add(ram_get);
        if (ram.Count > 20)
        {
            ram.RemoveAt(0);
        }
        temperature.Add(temperature_get);
        if (temperature.Count > 20)
        {
            temperature.RemoveAt(0);
        }
    }

    public static void RenderGraph()
    {
        double maxElCpu = 0, maxIdCpu = 0;

        double[] cpuMaxList = new double[10];
        double[] ramMaxList = new double[10];
        double[] temperatureMaxList = new double[10];

        List<double> tempCpu = new List<double>(cpu);
        List<double> tempRam = new List<double>(ram);
        List<double> tempTemperature = new List<double>(temperature);
        tempCpu.Sort();
        tempRam.Sort();
        tempTemperature.Sort();

        if (cpu.Count == 20 && ram.Count == 20 && temperature.Count >= 20)
        {
            Console.WriteLine("================================CPU GRAPH================================");
            Console.Write("   " + $"{tempCpu[10]:00.0}" + "|");
            for (int i = 10; i < 20; i++)
            {
                if (i != 10) {Console.Write($"{tempCpu[i]:00.0}|"); }
                cpuMaxList[i-10] = tempCpu[i]; 
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.Write((i + 1).ToString() + "|"); 
                }
                else
                {
                    Console.Write((i + 1).ToString() + " |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (cpu[i] >= cpuMaxList[j])
                    {
                        Console.Write("████ ");
                    }
                    else if (j == 0)
                    {
                        Console.Write("████ ");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("================================RAM GRAPH================================");
            Console.Write("   " + $"{tempRam[10]:00.0}" + "|");
            for (int i = 10; i < 20; i++)
            {
                if (i != 10) { Console.Write($"{tempRam[i]:00.0}|"); }
                ramMaxList[i - 10] = tempRam[i];
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.Write((i + 1).ToString() + "|");
                }
                else
                {
                    Console.Write((i + 1).ToString() + " |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (ram[i] >= ramMaxList[j])
                    {
                        Console.Write("████ ");
                    }
                    else if (j == 0)
                    {
                        Console.Write("████ ");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("================================TEMEPRATURE GRAPH================================");
            Console.Write("   " + $"{tempTemperature[10]:00.0}" + "|");
            for (int i = 10; i < 20; i++)
            {
                if (i != 10) { Console.Write($"{tempTemperature[i]:00.0}|"); }
                temperatureMaxList[i - 10] = tempTemperature[i];
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.Write((i + 1).ToString() + "|");
                }
                else
                {
                    Console.Write((i + 1).ToString() + " |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (temperature[i] >= temperatureMaxList[j])
                    {
                        Console.Write("████ ");
                    }
                    else if (j == 0)
                    {
                        Console.Write("████ ");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Waiting for data");
        }
    }
    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        Console.Clear();

        Console.WriteLine($"CPU: {cpu}%");
        Console.WriteLine($"RAM: {ram}%");
        Console.WriteLine($"Temperature: {temperature} °C");

        SaveData(cpu, ram, temperature);
        RenderGraph();
    }
}