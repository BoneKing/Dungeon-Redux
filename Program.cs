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
            while (gameRunning == true)
            {
                Time time = new Time();
                time.initTime();
                time.runTime();
                for(int i =0;i<100;i++){
                    time.printTime();
                }
                Console.WriteLine("DONE!");
                gameRunning = false;
            }
        }
    }
}
