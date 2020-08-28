using System;
using System.Threading;
using System.Collections.Generic;

namespace Dungeon_Redux
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random;
            int numEnemies = 4; //actual num +1;
            /*
            struct HighScore{
                int highScore = 0;
                String highScoreName = "None";
            }
            */
            Time time = new Time();
            time.initTime();
            //UNCOMMENT THE LINES BELOW TO START THE CLOCK
            //Thread timeThread = new Thread(new ThreadStart(time.runTime));
            //timeThread.IsBackground = true;
            //timeThread.Start();
            Player p1 = new Player();
            p1.NewPlayer();
            //GAME LOOP
            Console.WriteLine("\nWelcome to the Dungeon \n Survive all 7 days to win!");
            Console.WriteLine("\nWould you like the Tutorial? y/n");
            if(Console.ReadLine() == "y"){
                Tutorial(p1);
            }
            while (!p1.getdead() || !time.endTime())
            {
                p1.CalculateHungry(time.day, time.hour);
                if(p1.dead == true){
                    break;
                }
                Console.WriteLine("\n1. Walk deeper into the cave.");
                Console.WriteLine("2. Eat some food.");
                Console.WriteLine("3. Rest.");
                Console.WriteLine("4. Quit");
                switch(Console.ReadLine()){
                    case "1":
                        random = new Random();
                        int whatE = random.Next(1, numEnemies);
                        //Console.WriteLine("whatE = {0}", whatE);
                        Enemy e = newEnemy(whatE);
                        Console.WriteLine("You decide to keep walking further into the deps.");
                        Console.WriteLine("All of the sudden you get ambushed by a {0}", e.name);
                        Battle(p1, e);
                        if(p1.dead == true){
                            break;
                        }
                        p1.score++;
                        Console.WriteLine("Score = {0}", p1.score);
                        int whatToDrop = random.Next(1,3);
                        if(whatToDrop == 1){
                            int item = e.DropItem();
                            if(item == 0){ //no item dropped
                                Console.WriteLine("Nothin useful was found");
                                break; 
                            }
                            else if(item == 1) { //food dropped
                                Console.WriteLine("{0} dropped some food!", e.name);
                                p1.numFood++;
                            }
                            else if(item == 2) { //health potion Dropped
                                Console.WriteLine("{0} dropped a health potion!", e.name);
                                p1.numHealthPotions++;
                            }
                            else {
                                Console.WriteLine("ERROR: invalid item dropped, item num = {0}", whatToDrop);
                            }
                        }
                        else if(whatToDrop == 2){
                            Weapon NewWeapon = e.DropWeapon();
                            Console.WriteLine("{0} dropped a {1}", e.name, NewWeapon.name);
                            p1.getWeapon(NewWeapon);
                        }
                        else{
                            Console.WriteLine("ERROR: No Drop Option with number {0}", whatToDrop);
                        }
                        time.hour++;
                        break;
                    case "2":
                        if(p1.numFood > 0){
                            Console.WriteLine("You ate some food, it helps keep away the hunger");
                            p1.eat(time.day, time.hour);
                        }
                        else{
                            Console.WriteLine("You have no food to eat");
                        }
                        break;
                    case "3":
                        Console.WriteLine("You decide to take a break for a bit");
                        random = new Random();
                        int sleepAttack = random.Next(1,21);
                        if(sleepAttack < 20){
                            time.hour+=4;
                            p1.stamina+=2;
                        }
                        else{
                            time.hour+=2;
                            Console.WriteLine("You hear something in your sleep. You awaken to find you are surrounded by shadowy figures!");
                            Console.WriteLine("You're able to fend them off but you did take some damage");
                            p1.health = Convert.ToInt32(Math.Floor(p1.health*.75));
                        }
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
            Console.WriteLine("Score: {0}", p1.score);
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
                        int weapon = WeaponSelectMenu(p1);
                        tb.takeDamage(p1.Attack(weapon));
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
        static int WeaponSelectMenu(Player p1){
            Console.WriteLine("--------- Choose Your Weapon ---------");
            int index = 1; //what number weapon is it
            for(int i = 0; i < p1.WeaponList.Length; i++){
                if(p1.WeaponList[i].name != "Empty"){
                    Console.WriteLine("{0}. {1}", index, p1.WeaponList[i].name);
                    index++;
                }
            }
            //Console.WriteLine("Out of loop");
            string selStr = Console.ReadLine();
            int selInt = Convert.ToInt32(selStr);
            if(selInt >= p1.WeaponList.Length){
                Console.WriteLine("You reach for an imaginary weapon, get a hold of yourself!");
                selInt = 1;
            }
            return selInt-1;
        }
        static void Battle(Player p1, Enemy e){
            while(e.getHealth() > 0){
                switch(Menu(p1, e)){
                    case "1":
                        int weapon = WeaponSelectMenu(p1);
                        e.takeDamage(p1.Attack(weapon));
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
                                p1.stamina--;
                                p1.score--;
                                return;
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
                    p1.dead = true;
                    return;
                }
            }
        } 
        static Enemy newEnemy(int index){
            switch(index){
                case 1:
                    Boar b = new Boar();
                    b.NewBoar();
                    return b;
                case 2:
                    Goblin g = new Goblin();
                    g.NewGoblin();
                    return g;
                case 3:
                    SuspicousRock rock = new SuspicousRock();
                    rock.NewRock();
                    return rock;
                default:
                    Console.WriteLine("ERROR: No enemy found at index {0}", index);
                    Console.WriteLine("You get a Tutorial Bunny for breaking the game");
                    TutorialBunny tbBad = new TutorialBunny();
                    tbBad.NewBunny();
                    return tbBad; 
            }
        }
    }
}
