using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using Enemies;
using Weapons;
using Spells;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Dungeon_Redux
{
    class Program
    {
        static void Main(string[] args)
        {
            //Check for Update
            Update update = new Update();
            update.StartUpdate();
            //var updateProcess = System.Diagnostics.Process.Start(@"..\win10-x64-Update\Update.exe");
            //updateProcess.WaitForExit();
            //begin game setup
            Random random;
            //bool gameOver = false;
            //int numEnemies = 4; //actual num +1;
            /*
            struct HighScore{
                int highScore = 0;
                String highScoreName = "None";
            }
            */
            Time time = new Time();
            time.initTime();
            //UNCOMMENT THE LINES BELOW TO START THE CLOCK
            Thread timeThread = new Thread(new ThreadStart(time.runTime));
            timeThread.IsBackground = true;
            timeThread.Start();
            Player p1 = new Player();
            p1.NewPlayer();
            p1.APPointPlacement();
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
                        //int whatE = random.Next(1, numEnemies);
                        //Console.WriteLine("whatE = {0}", whatE);
                        Enemy e = newEnemy(time.day, time.hour);
                        if(e.name.Contains("Chad")){
                            FinalBoss(p1, e);
                            p1.score+=2;
                            Console.WriteLine("Score = {0}", p1.score);
                            return;
                        }
                        Console.WriteLine("You decide to keep walking further into the depths.");
                        Console.WriteLine("All of the sudden you get ambushed by a {0}", e.name);
                        Battle(p1, e);
                        if(e.name.Contains("Boss")){
                            time.day++;
                            time.hour = 0;
                        }
                        if(p1.dead == true){
                            break;
                        }
                        p1.score++;
                        p1.Exp(1*e.area);
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
                            if(NewWeapon.name != "Empty"){
                                Console.WriteLine("{0} dropped a {1}", e.name, NewWeapon.name);
                                p1.getWeapon(NewWeapon);
                            }
                            else if(NewWeapon.name == "Empty"){
                                Console.WriteLine("Nothin useful was found");
                            }
                        }
                        else{
                            Console.WriteLine("ERROR: No Drop Option with number {0}", whatToDrop);
                        }
                        time.hour++;
                        Console.WriteLine("day {0} at hour {1}", time.day, time.hour);
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
                        int sleepAttack = random.Next(0,10);
                        if(sleepAttack < 8){
                            p1.HealFist();
                            if(time.hour+4 > 23){
                                time.hour = 23;
                            }
                            else{
                                time.hour+=4;
                            }
                            p1.stamina+=2;
                            p1.health+=15;
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
            tb.Create();
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
                        int spell = SpellSelectMenu(p1);
                        if(spell == -1){ //no spells to cast
                            tb.takeDamage(0);
                        }
                        else{
                            tb.takeDamage(p1.UseSpell(spell));
                        }
                        if(tb.getHealth() < 1){
                            break;
                        }
                        p1.health = p1.health - tb.Attack();
                        break;
                    case "3":
                        if(p1.numHealthPotions > 0){
                            p1.heal();
                        }
                        p1.health = p1.health - tb.Attack();
                        break;
                    case "4":
                        p1.health = p1.health - Convert.ToInt32(Math.Floor((0.5 * tb.attackDmg)));
                        break;
                    case "5":
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
                    p1.dead = true;
                    return;
                }
            }
            Console.WriteLine("\nEh, you get it now right? alright, this is the end of the tutorial. Good Luck, you'll need it.");
        }
        static string Menu(Player p1, Enemy e){
            Console.WriteLine("\n {0} \t HP: {1}", e.name, e.health);
            Console.WriteLine("\n HP: {0} \t MP: {1} \t ST: {2} \t Potions: {3} \t Level: {4}", p1.health, p1.MP, p1.stamina, p1.numHealthPotions, p1.Lvl);
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Cast a Spell");
            Console.WriteLine("3. Use Health Potion");
            Console.WriteLine("4. Defend");
            Console.WriteLine("5. Run");
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
            string selStr; 
            //make sure input is a number
            while(true){
                selStr = Console.ReadLine();
                if(String.Compare(selStr, "0") > 0 && String.Compare(selStr, "9") < 0){
                    break;
                }
            }
            int selInt = Convert.ToInt32(selStr);
            Console.WriteLine(selInt);
            if(selInt >= p1.WeaponList.Length){
                Console.WriteLine("You reach for an imaginary weapon, get a hold of yourself!");
                selInt = 1;
            }
            return selInt-1;
        }
        static int SpellSelectMenu(Player p1){
            int index = 1; //what number spell is it
            Console.WriteLine("--------- Choose Your Spell ---------");
            for(int i = 0; i < p1.SpellBook.Length; i++){
                if(p1.SpellBook[i].name != ""){
                    Console.WriteLine("{0}. {1} \t {2}", index, p1.SpellBook[i].name, p1.SpellBook[i].description);
                    index++;
                }
            }
            if(index == 1){
                Console.WriteLine("You reach your hand out and shout gibberish... nothing happened...");
                return -1;
            }
            //Console.WriteLine("Out of loop");
            string selStr = Console.ReadLine();
            int selInt = Convert.ToInt32(selStr);
            if(selInt >= p1.SpellBook.Length){
                Console.WriteLine("You reach your hand out and shout gibberish... nothing happened...");
                selInt = 1;
            }
            return selInt-1;
        }
        static void Battle(Player p1, Enemy e){
            while(e.getHealth() > 0){
                switch(Menu(p1, e)){
                    case "1": //Attack 
                        int weapon = WeaponSelectMenu(p1);
                        e.takeDamage(p1.Attack(weapon));
                        //Console.WriteLine(weapon);
                        if(e.name == "Suspicous Rock" && weapon == 0){
                            p1.BreakFist();
                        }
                        if(e.getHealth() < 1){
                            break;
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    case "2":
                        int spell = SpellSelectMenu(p1);
                        //Console.WriteLine("spell = {0}", spell);
                        if(spell == -1){ //no spells to cast
                            e.takeDamage(0);
                        }
                        else{
                            e.takeDamage(p1.UseSpell(spell));
                        }
                        if(e.getHealth() < 1){
                            break;
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    case "3": //Heal
                        if(p1.numHealthPotions > 0){
                            p1.heal();
                        }
                        p1.health = p1.health - e.Attack();
                        break;
                    case "4": //Defend
                        p1.health = p1.health - Convert.ToInt32(Math.Floor((0.5 * e.Attack())) + (0.33*p1.stats["defence"]));
                        break;
                    case "5": //Run
                        Console.WriteLine("You look around youu for a way out of this fight");
                        if(p1.GetSpeed() > e.speed){
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
        static Enemy newEnemy(int day, int hour){
            Random random = new Random();
            //var file = Directory.GetFiles("Enemies","*.cs");
            //Console.WriteLine("Enemy File = {0}", file);
            int index = 0;
            if(day > 0 && day < 3){
                if(day == 2 && hour >= 20){
                    Console.WriteLine("Bear time");
                    index = 7;
                }
                else{
                    index = random.Next(1,7);
                }
            }
            else if(day >= 3 && day < 5){
                if(day == 4 && hour >= 20){
                    Console.Clear();
                    Console.WriteLine("*You see that the cave comes to a point where 20 foot steel doors stand in your way to the next area.*");
                    Console.WriteLine("*As you approach the door you hear a deep voice saying 'None shall pass through the gates of Hell without permission!'*");
                    Console.WriteLine("*Huge flames suddenly appear before the door and a giant red man draped in chains and breathing fire appears, wielding a giant hammer!*");
                    index = 12;
                }
                else{
                    index = random.Next(8, 12);
                }
            }
            else if(day >= 5 && day < 7){
                if(day == 6 && hour >= 20){
                    if(random.Next(1,3) == 1){
                        Console.Clear();
                        Console.WriteLine("As you walk deeper into hell you see a 50 ft dog with 3 heads!");
                        Console.WriteLine("He paces back and forth in from of a bridge that leads even deeper into the depths of hell.");
                        Console.WriteLine("Welp, we made it this far, might as well go farther.");
                        index = 17;
                    }
                    else{
                        Console.Clear();
                        Console.WriteLine("As you walk deeper into hell you see a 50 ft dog with 3 heads!");
                        Console.WriteLine("He paces back and forth in front of a bridge that leads even deeper into the depths of hell.");
                        Console.WriteLine("Welp, we made it this far, might as well go farther.");
                        Console.WriteLine("Wait! What's that to the left?");
                        Console.WriteLine("Another bridge! There seems to be some dinosaur, like a Pterodactyl?");
                        Console.WriteLine("You go and approach the Pterodactyl.");
                        index = 18;
                    }
                }
                else{
                    index = random.Next(13, 17);
                }
            }
            else if(day > 6){
                if((day == 7 && hour >= 20) || day > 7){
                    Console.Clear();
                    Console.WriteLine("You see off in the distance a demonic castle.");
                    Console.WriteLine("You run to it bursting through the gran1d doors into the main hall");
                    Console.WriteLine("There on a throne you see a huge dark demon king with a crown");
                    index = 23;
                }
                else{
                    index = random.Next(19, 23);
                }
            }
            switch(index){
                case 1:
                    Boar b = new Boar();
                    b.Create();
                    return b;
                case 2:
                    Wolf w = new Wolf();
                    w.Create();
                    return w;
                case 3:
                    SuspicousRock rock = new SuspicousRock();
                    rock.Create();
                    return rock;
                case 4:
                    Bats bats = new Bats();
                    bats.Create();
                    return bats;
                case 5:
                    Bunny bun = new Bunny();
                    bun.Create();
                    return bun;
                case 6:
                    Snake s = new Snake();
                    s.Create();
                    return s;
                case 7:
                    Bear bear = new Bear();
                    bear.Create();
                    return bear;
                case 8:
                    SuspicousRock rock2 = new SuspicousRock();
                    rock2.Create();
                    return rock2;
                case 9:
                    Goblin g = new Goblin();
                    g.Create();
                    return g;
                case 10:
                    Hobgoblin hg = new Hobgoblin();
                    hg.Create();
                    return hg;
                case 11:
                    SavageBarbarian sb = new SavageBarbarian();
                    sb.Create();
                    return sb;
                case 12:
                    HellsGatekeeper HGK = new HellsGatekeeper();
                    HGK.Create();
                    return HGK;
                case 13: 
                    Demon De = new Demon();
                    De.Create();
                    return De;
                case 14:
                    DemonicBoar DB = new DemonicBoar();
                    DB.Create();
                    return DB;
                case 15:
                    SuspicousRock rock3 = new SuspicousRock();
                    rock3.Create();
                    return rock3;
                case 16:
                    Pheonix p = new Pheonix();
                    p.Create();
                    return p;
                case 17:
                    Cerberus C = new Cerberus();
                    C.Create();
                    return C;
                case 18:
                    Peter peter = new Peter();
                    peter.Create();
                    return peter;
                case 19:
                    DemonGeneral DG = new DemonGeneral();
                    DG.Create();
                    return DG;
                case 20:
                    SavageCaveDweller SCD = new SavageCaveDweller();
                    SCD.Create();
                    return SCD;
                case 21:
                    SuspicousRock rock4 = new SuspicousRock();
                    rock4.Create();
                    return rock4;
                case 22:
                    Dragon dr = new Dragon();
                    dr.Create();
                    return dr;
                case 23:
                    Chad chad = new Chad();
                    chad.Create();
                    return chad;
                default:
                    Console.WriteLine("\nERROR: No enemy found at index {0}", index);
                    Console.WriteLine("You get a Tutorial Bunny for breaking the game");
                    TutorialBunny tbBad = new TutorialBunny();
                    tbBad.Create();
                    return tbBad; 
            }
        }
        static public void FinalBoss(Player p1, Enemy e){
            Battle(p1, e);
            if(p1.health < 1){
                return;
            }
            TutorialBunny2 tb2 = new TutorialBunny2();
            tb2.Create();
            Battle(p1, tb2);
            if(p1.health < 1){
                return;
            }
            Console.Clear();
            Console.WriteLine("Chad falls to the floor, having used all of his energy he turns to chrimson dust and blows away");
            Console.WriteLine("Congrats, you are now the king of Hell.");
            Console.WriteLine("#############################################");
            Console.WriteLine("#             Thanks For Playing            #");
            Console.WriteLine("#        Find this project on GitHub        #");
            Console.WriteLine("# https://github.com/BoneKing/Dungeon-Redux #");
            Console.WriteLine("#     This game was made by Andy Mahoney    #");
            Console.WriteLine("#            Last Updated 10/20/2020        #");
            Console.WriteLine("#############################################");
        }
    }
}
