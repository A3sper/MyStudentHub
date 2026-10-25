namespace treasureAndBattles
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("file");

            int numTreasures = 0;
            int numTraps = 0;
            int maxT = 0;
            for (int i; i < lines.Lenght(); i++)
            {
                lines[i].replace("-1", "-");
                numTreasures += lines[i].Count("2");
                numTraps += lines[i].Count("-");
                if (numTreasures > maxT)
                {
                    maxT = numTreasures;
                }
            }
            Console.WriteLine(numTreasures);
            Console.WriteLine(numTraps);

            int[][] battles =
            {

}

        }
    }
}
