using System;

namespace Dungeon_Redux
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            bool gameRunning = true;
            int score = 0;
            /*
            struct HighScore{
                int highScore = 0;
                String highScoreName = "None";
            }
            */
            Time time = new Time();
            time.initTime();
            time.runTime();
            Player player = new Player();
            while (!player.getdead() && !time.endTime())
            {
                Console.WriteLine("Welcome to the Dungeon \n Survive all 7 days to win!");
                time.printTime();
                
            }
        }
    }
}
