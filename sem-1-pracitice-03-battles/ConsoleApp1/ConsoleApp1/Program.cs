namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("test.txt");

            int numTreasures = 0;
            int numTraps = 0;
            int treasure = 0;
            int maxT = 0;
            int indexLine = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Replace("-1", "-");
                numTreasures += lines[i].Count(c => c == '2');
                numTraps += lines[i].Count(c => c == '-');
                treasure = lines[i].Count(c => c == '2');
                if (treasure > maxT)
                {
                    maxT = numTreasures;
                    indexLine = i;
                }
            }
            Console.WriteLine(numTreasures); 
            Console.WriteLine(numTraps);
            Console.WriteLine(lines[indexLine]);

            int[][] battles =
            {
                new []{1, 2, 3},
                new []{30, 25},
                new []{40}
            };


            int playerWithMaxPoints = 0;
            float  playerMidPoints = 0f;
            int points, maxPoints = 0;
            int maxBattle = 0;
            for(int i = 0; i < battles.Length; i++) 
            {
                points = 0;
                for(int j = 0; j < battles[i].Length; j++)
                {
                    points += battles[i][j];
                    if (battles[i][j] > maxBattle)
                    {
                        maxBattle = battles[i][j];
                    }
                }
                if (points > maxPoints)
                {
                    maxPoints = points;
                    playerWithMaxPoints = i;
                }
                if (points / battles[i].Length > playerMidPoints)
                {
                    playerMidPoints = points / battles[i].Length;
                    
                }
            }
            Console.WriteLine(battles[playerWithMaxPoints]);
            Console.WriteLine(playerMidPoints);
            Console.WriteLine(maxBattle);
        }
    }
}
