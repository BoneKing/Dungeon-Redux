﻿using System;
using System.Threading;

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
            Thread timeThread = new Thread(new ThreadStart(time.runTime));
            timeThread.IsBackground = true;
            timeThread.Start();
            //time.runTime();
            Player p1 = new Player();
            p1.NewPlayer();
            Console.WriteLine("HERE");
            Console.WriteLine(p1.getdead());
            Console.WriteLine(time.endTime());
            //GAME LOOP
            while (!p1.getdead() && !time.endTime())
            {
                Console.WriteLine("Welcome to the Dungeon \n Survive all 7 days to win!");
                Console.WriteLine("Would you like the Tutorial? y/n");
                if(Console.ReadLine() == "y"){
                    Tutorial(p1);
                }

            }
        }
        static void Tutorial(Player p1){
            Console.WriteLine("In this game you'll find yourself fighting enemies at random times");
            Console.WriteLine("you must manage your stamina and health well iff you expect to last all 7 days");
            Console.WriteLine("Oh Look! An enemy ... er something");
            TutorialBunny tb = new TutorialBunny();
            tb.NewBunny();
            while(tb.getHealth() > 0){
                Console.WriteLine("\n Tutorial Bunny \t HP: {0}", tb.health);
                switch(Menu(p1)){
                    case "1":
                        tb.takeDamage(p1.attackDamage);
                        if(tb.getHealth() < 1){
                            break;
                        }
                        p1.health = p1.health - tb.Attack();
                        break;
                    case "2":
                        if(p1.numHealthPotions > 0){
                            p1.heal();
                        }
                        p1.health = p1.health - tb.Attack();
                        break;
                    case "3":
                        p1.health = p1.health - Convert.ToInt32(Math.Floor((0.5 * tb.attackDmg)));
                        break;
                    case "4":
                        Console.WriteLine("Really!? You're running away in a Tutorial Fight!?");
                        Console.WriteLine("I don't think so! Man Up!");
                        p1.health = p1.health - tb.Attack();
                        break;
                    default:
                        Console.WriteLine("What are you stupid!? Pick an actual option");
                        break;
                }
            }
        }
        static string Menu(Player p1){
            Console.WriteLine("\n HP: {0} \t ST: {1} \t Potions: {2}", p1.health, p1.stamina, p1.numHealthPotions);
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Health Potion");
            Console.WriteLine("3. Defend");
            Console.WriteLine("4. Run");
            return Console.ReadLine();
        }
    }
}
