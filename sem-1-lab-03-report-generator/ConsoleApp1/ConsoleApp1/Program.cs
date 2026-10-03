

using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private static string password = "Hello213";

        static void Main(string[] args)
        {
            int playerNum= 102032130;
            float RAMavailable = 1f;
            string serverName = "FunGames.com";
            DateTime dateTime = DateTime.Now;
            bool hasPassword = false;
            string gameOnServer = "Buckshot Roulette";

            string status = canStart(playerNum, RAMavailable, hasPassword);
            PrintData(playerNum, RAMavailable, serverName, dateTime, hasPassword, gameOnServer, status);
        }

        private static void PrintData(int playerNum, float RAM, string serverName, DateTime now, bool hasPassword, string gameOnServer, string status)
        {
            Console.WriteLine($"╰┈➤=================「 ✦SERVER NAME: {serverName}✦ 」=================");
            Console.WriteLine($"┃ Game on Server {gameOnServer}");
            Console.WriteLine($"┃ Today is {now}");
            Console.WriteLine($"┃ Players on server {playerNum} and available RAM is {RAM}");
            Console.WriteLine($"┃ Server has password is {hasPassword}");
            Console.WriteLine($"★ Conclusion Status is `{ status}`");
        }
        private static bool isEnough(int playerNum, float RAM) 
        { 
            if (playerNum/2 <= RAM)
            {
                return true;
            }
            return false;
        }
        private static string canStart(int playerNum, float RAM, bool hasPassword)
        {
            if (isEnough(playerNum, RAM) == true && hasPassword == false) 
            {
                return "Everything is fine";
            }
            else if (isEnough(playerNum, RAM) == false)
            {
                return "NotEnoughMemory, failed to start";
            }
            else if (hasPassword == true)
            {
                Console.WriteLine("Please enter the password");
                string enter = Console.ReadLine();
                if (enter == password)
                {
                    return "Everything is fine, connection is protected";
                }
                return "Server isn't running, because of wrong password";
            }
            return "Something went wrong :/";
        }
    }
}
