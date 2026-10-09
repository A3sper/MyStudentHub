using System.Globalization;
using System.Runtime.InteropServices;

namespace TempProject;

internal class Program
{

    // 2026-09-01 12:16:01.101 [Info][User] User logged in: RavenFox
    // 2026-09-01 12:16:07.893 [Info][Server] Event "Frostfire Rebellion" scheduled to start in 2 hours
    // 2026-09-01 12:18:25.105 [Info][Event] Event started

    public static DateTime GetLogDateTime(string line)
    {
        // 2026-09-01 12:16:01.101 [Info][User] User logged in: RavenFox

        int index1 = line.IndexOf(' ');
        Console.WriteLine(index1);
        int index2 = line.IndexOf(' ', index1 + 1);
        Console.WriteLine(index2);

        string datetimeStr = line.Substring(0, index2);
        Console.WriteLine(datetimeStr);

        // "1.2"
        // "1,2"

        float num = float.Parse("1,2");

        DateTime dateTime = DateTime.Parse(datetimeStr);
        return dateTime;

        //string[] parts = line.Split([' ', '[', ']'], StringSplitOptions.RemoveEmptyEntries);
        //foreach (string part in parts)
        //{
        //    Console.WriteLine(part);
        //}
    }

    /*public static string GetLogLevel(string line)
    {

    }

    public static string GetLogType(string line)
    {

    }

    public static string GetLogText(string line)
    {

    }
    */
    // tuple
    // Tuple<DateTime, string, string, string>
    public static (DateTime, string, string, string) GetLog(string line)
    {
        string[] getFirstLevel = line.Split("] ");
        string text = getFirstLevel[1];
        string[] getSecondLevel = getFirstLevel[0].Split("[");
        string level = getSecondLevel[1][..^1];
        string type = getSecondLevel[2];

        DateTime now = DateTime.Parse(getSecondLevel[0][..^1]);
        return (now, level, type, text);
    }

    public static void Main()
    {
        //string file = File.ReadAllText("test1.txt");
        //string[] lines = File.ReadAllLines();

        //Console.Write("Hello\n\tWorld");

        //Console.WriteLine(File.ReadAllText("test1.txt"));
        //Console.WriteLine(File.ReadAllText("../test2.txt"));
        //if (File.Exists("../../../test3.txt"))
        //{
        //    // ...
        //}
        //else
        //{

        //}

        //Console.WriteLine(File.ReadAllText("../../../configs/test3.txt"));

        //string path = "\"D:\\dev\\StudentCs\\code-examples\\TempProject\\FileName.cs\"";
        //Console.WriteLine(path);

        string[] lines = File.ReadAllLines("event_server.log.txt");
        foreach (string line in lines)
        {
            Console.WriteLine(GetLog(line));
        }

        for (int i = 0; i < lines.Length; i++)
        {
        }

        // sem-1-practice-2-log-parse
    }
}
