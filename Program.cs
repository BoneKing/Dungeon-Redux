﻿using System;
using System.Threading;

namespace Dungeon_Redux
{
    class Program
    {
        static void Main(string[] args)
        {
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
            //GAME LOOP
            Console.WriteLine("\nWelcome to the Dungeon \n Survive all 7 days to win!");
            Console.WriteLine("\nWould you like the Tutorial? y/n");
            if(Console.ReadLine() == "y"){
                Tutorial(p1);
            }
            while (!p1.getdead() && !time.endTime())
            {
                Console.WriteLine("1. Walk deeper into the cave.");
                Console.WriteLine("2. Eat some food.");
                Console.WriteLine("3. Rest.");
                Console.WriteLine("4. Quit");
                switch(Console.ReadLine()){
                    case "1":
                        TutorialBunny e = new TutorialBunny();
                        e.NewBunny();
                        Console.WriteLine("You decide to keep walking further into the deps.");
                        Console.WriteLine("All of the sudden you get ambushed by a {0}", e.name);
                        Battle(p1, e);
                        break;
                    case "2":
                        if(p1.numFood > 0){
                            Console.WriteLine("You ate some food, it helps keep away the hunger");
                            p1.numFood--;
                            p1.hungerCounter--;
                        }
                        else{
                            Console.WriteLine("You have no food to eat");
                        }
                        break;
                    case "3":
                        Console.WriteLine("You decide to take a break for a bit");
                        time.hour+=4;
                        p1.stamina++;
                        break;
                    case "4":
                        Console.WriteLine("Are you sure you want to quit? y/n");
                        if(Console.ReadLine() == "y"){
                            return;
                        }
                        else{
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid option, try something else.");
                        break;

                }
            }
        }
        static void Tutorial(Player p1){
            Console.WriteLine("\nIn this game you'll find yourself fighting enemies at random times");
            Console.WriteLine("you must manage your stamina and health well if you expect to last all 7 days");
            Console.WriteLine("Oh Look! An enemy ... er something");
            TutorialBunny tb = new TutorialBunny();
            tb.NewBunny();
            while(tb.getHealth() > 0){
                switch(Menu(p1, tb)){
                    case "1":
                        tb.takeDamage(p1.Attack());
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
                if (p1.health < 1){
                    Console.WriteLine("WHAT!? YOU DIED IN THE TUTORIAL!? wow ... just wow.");
                    return;
                }
            }
            Console.WriteLine("\nEh, you get it now right? alright, this is the end of the tutorial. Good Luck, you'll need it.");
        }
        static string Menu(Player p1, Enemy e){
            Console.WriteLine("\n {0} \t HP: {1}", e.name, e.health);
            Console.WriteLine("\n HP: {0} \t ST: {1} \t Potions: {2}", p1.health, p1.stamina, p1.numHealthPotions);
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Health Potion");
            Console.WriteLine("3. Defend");
            Console.WriteLine("4. Run");
            return Console.ReadLine();
        }
        static void Battle(Player p1, Enemy e){
            while(e.getHealth() > 0){
                switch(Menu(p1, e)){
                    case "1":
                        e.takeDamage(p1.Attack());
                        if(e.getHealth() < 1){
                            break;
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    case "2":
                        if(p1.numHealthPotions > 0){
                            p1.heal();
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    case "3":
                        p1.health = p1.health - Convert.ToInt32(Math.Floor((0.5 * e.Attack())));
                        break;
                    case "4":
                        Console.WriteLine("You look around youu for a way out of this fight");
                        if(p1.speed > e.speed){
                            if(p1.stamina > 0){
                                Console.WriteLine("there's an opening and you run for it!");
                                Console.WriteLine("You made it away!");
                                p1.stamina = p1.stamina -1;
                            }
                            else{
                                Console.WriteLine("You're too tired to run");
                            }
                        }
                        else {
                            Console.WriteLine("You try to run but the {0} is too fast for you!", e.name);
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    default:
                        Console.WriteLine("What are you stupid!? Pick an actual option");
                        break;
                }
                if (p1.health < 1){
                    Console.WriteLine("Looks like you died.");
                    return;
                }
            }
        } 
    }
}
