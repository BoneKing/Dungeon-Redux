using System;
using System.Collections; 
using System.Collections.Generic; 
using Enemies;
using Weapons;
using Spells;

namespace Dungeon_Redux
{
    public class Player{
        Random random;
        public string name;
        public int health; //players health
        int maxHealth; //max health
        public bool dead; //is the player dead
        public int attackDamage; //base attack damage
        public int numHealthPotions; //number of health potions held
        int healthPotionHealAmount; //how strong the health potion is
        public int numFood; //how much food the player has
        public int enemiesKilled; //how many enemies have been defeated 
        public int stamina; //player stamina 
        public int speed; //speed stat to get away (maybe dodge later)
        public bool running; //can you run?
        public int hungerCounter; //how hungry you are
        public Weapon[] WeaponList = new Weapon[5];
        public Spell[] SpellBook = new Spell[5];
        public int score;
        public int TotalHourAte; //day and hour as hours since eaten last
        public int waitHungerWarning;
        public int exp;
        public int expToNextLevel;
        public int Lvl;
        public int AP;
        public int MP; 
        public Dictionary<string, int> stats = new Dictionary<string, int>();
        public void NewPlayer(){ //init player
            health = 100;
            maxHealth = 100;
            dead = false;
            attackDamage = 7;
            numHealthPotions = 3;
            healthPotionHealAmount = 35;
            numFood = 100;
            enemiesKilled = 0; // part of score
            stamina = 5;
            speed = 5;
            running = true;
            hungerCounter = 0;
            Weapon Fist = new Fists();
            Fist.Create();
            WeaponList[0]=Fist;
            Weapon EmptySlot = new EmptyWeaponSlot();
            EmptySlot.Create();
            WeaponList[1]=EmptySlot;
            WeaponList[2]=EmptySlot;
            WeaponList[3]=EmptySlot;
            WeaponList[4]=EmptySlot;
            Spell Empty = new EmptySpellSlot();
            Empty.Create();
            SpellBook[0] = Empty;
            SpellBook[1] = Empty;
            SpellBook[2] = Empty;
            SpellBook[3] = Empty;
            SpellBook[4] = Empty;
            score = 0;
            TotalHourAte = 0;
            waitHungerWarning = 0;
            exp = 0;
            expToNextLevel = 2;
            Lvl = 1;
            MP = 25;
            AP = 5;
            stats.Add("strength", 0);
            stats.Add("speed", 0);
            stats.Add("health", 0);
            stats.Add("defence", 0);
            stats.Add("magic", 0);
            stats.Add("precision", 0);
            stats.Add("luck", 0);
            Console.WriteLine("Welcome young traveller! What do you want to be called?");
            name = Console.ReadLine();
            if(name == "Narpa"){
                health = 99999;
                Weapon NarpasSword = new NarpasSword();
                NarpasSword.Create();
                WeaponList[0] = NarpasSword;
                stamina = 999;
                speed = 999;
            }
        }
        public bool getdead(){
            //Console.WriteLine("health = {0}", health);
            if (health < 1 || hungerCounter > 4){
                return true;
            }
            else{
                return false;
            }
        }
        public void hungry(){ //increments hunger counter
            Console.WriteLine("\nYou have become Hungry");
            hungerCounter++;
            if(hungerCounter == 4){
                Console.WriteLine("You're starving!"); 
            }
            else if(hungerCounter > 4){
                Console.WriteLine("The pain in your stomach is too much to take, your vision begins to fade as you feel all energy being sapped from your body");
                die();
            }

        }
        public void eat(int day, int hour){ //decrements hunger counter
            hungerCounter--;
            numFood--;
            int dayInHours = day*24;
            TotalHourAte = dayInHours+hour;
            if(hungerCounter < 1){
                Console.WriteLine("You're belly is filled with food");
            }
            else{
                Console.WriteLine("You feel slightly fuller");
            }
        }
        public void heal(){
            numHealthPotions--;
            Console.WriteLine("You now have {0} health potions remaining", numHealthPotions);
            health = health + healthPotionHealAmount;
            if(health > maxHealth){
                health = maxHealth;
            }
            Console.WriteLine("You now have {0} health", health);
        }
        public int GetSpeed(){
            int sp = speed + score + Convert.ToInt32(0.33*stats["speed"]);
            Console.WriteLine("Current Speed = {0}", sp);
            return sp;
        }
        public int Attack(int i){
            Weapon w = WeaponList[i];
            random = new Random();
            int dmg = 0;
            if(random.Next(0,9)+stats["luck"]/5 >= 8){
                dmg = Convert.ToInt32((w.baseDmg + w.highRange)*1.5);
                Console.WriteLine("CRITICAL HIT!");
            }
            else{
                dmg = w.baseDmg + random.Next(w.lowRange,w.highRange);
            }
            w.durability--;
            if(w.durability <= 0){
                Console.WriteLine("\n{0} Broke.", w.name);
                WeaponList[i] = new EmptyWeaponSlot();
                WeaponList[i].Create();
                SortWeaponList();
            }
            int totalDmg = Convert.ToInt32(dmg+(0.33*stats["strength"]));
            return totalDmg;
        }
        public int UseSpell(int i){
            Spell s = SpellBook[i];
            random = new Random();
            int dmg = 0;
            if((random.Next(0, 101) - stats["precision"]) > s.accuracy){ //miss
                Console.WriteLine("Your spell flies off course and misses!");
                MP -= s.MPCost;
                return dmg;
            }
            if(random.Next(0,9)+stats["luck"]/5 >= 8){
                dmg = Convert.ToInt32(s.baseDmg*1.5);
                Console.WriteLine("CRITICAL HIT!");
                MP -= s.MPCost;
                return dmg;
            }
            else {
                MP -= s.MPCost;
                return s.baseDmg;
            }
        }
        public bool die(){
            Console.WriteLine("You Died.\n");
            dead = true; 
            return getdead(); 
        }
        public void getWeapon(Weapon NewWeapon){
            for(int i = 0; i < WeaponList.Length; i++){ //replace Empty slot
                if(WeaponList[i].name == "Empty"){
                    WeaponList[i] = NewWeapon;
                    return;
                }
            }
            //if we get here then all slots are filled
            Console.WriteLine("You are carring as many weapons as you can, you need to drop one to pick the new weapon up.");
            Console.WriteLine("Which Weapon do you want to get rid of?");
            int index = 1; //what number weapon is it
            for(int i = 0; i < WeaponList.Length; i++){
                if(WeaponList[i].name != "Empty"){
                    Console.WriteLine("{0}. {1}", index, WeaponList[i].name);
                    index++;
                }
            }
            Console.WriteLine("6. Don't pick up the {0}", NewWeapon.name);
            //Console.WriteLine("Out of loop");
            string selStr = Console.ReadLine();
            int selInt = Convert.ToInt32(selStr);
            if(selInt == 6){
                return;
            }
            selInt--;
            Console.WriteLine("You dropped your {0} and picked up a {1}", WeaponList[selInt].name, NewWeapon.name);
            WeaponList[selInt] = NewWeapon;
            /* Console.WriteLine("Are you sure you wish to get rid of {0}", WeaponList[selInt].name);
            string ans = Console.ReadLine();
            if(ans == "y"){
                WeaponList[selInt] = NewWeapon;
            }
            */
        }
        public void CalculateHungry(int day, int hour){
            int daysInHours = (day-1)*24;
            int newHours = daysInHours + hour;
            if(newHours-6 <= 0){
                newHours = 0;
            }
            if(newHours - 6 >= TotalHourAte){ 
                if(waitHungerWarning %3 == 0){
                    hungry();
                    return;
                }
            }
            else{
                return;
            }
        }
        public void SortWeaponList(){
            for(int a = 0; a < 5; a++){
                int FirstEmptyIndex = -1;
                for(int i =0; i<WeaponList.Length;i++){
                    if(WeaponList[i].name == "Empty"){
                        FirstEmptyIndex = i;
                        break;
                    }
                }
                if(FirstEmptyIndex == -1){
                    return;
                }
                for(int i = FirstEmptyIndex+1; i<WeaponList.Length; i++){
                    if(WeaponList[i].name != "Empty"){
                        WeaponList[FirstEmptyIndex] = WeaponList[i];
                        WeaponList[i] = new EmptyWeaponSlot();
                        WeaponList[i].Create();
                    }
                }
            }
        }
        public void Exp(int ToGiveExp){
            exp += ToGiveExp;
            if(exp >= expToNextLevel){
                LevelUp();
            }
        }
        public void LevelUp(){
            exp = exp - expToNextLevel;
            expToNextLevel *= 2;
            maxHealth += 5*Lvl;
            stamina += 2;
            Lvl++;
            AP += 5;
            Console.WriteLine("\n**Leveled Up to Level {0}!**\n", Lvl);
            APPointPlacement();
            maxHealth += (10 * stats["health"]);
            health = maxHealth;
            NewSpells(stats["magic"]);
        }
        public void APPointPlacement(){
            var keys = new List<string>(stats.Keys);
            int[] statbuffs = new int[7];   
            while(AP > 0){
                int idx = 0;
                foreach (KeyValuePair<string, int> item in stats)
                {
                    Console.WriteLine("{0}: {1}", item.Key, item.Value);
                }
                Console.WriteLine("Available Attribute Points: {0}", AP);
                foreach (KeyValuePair<string, int> item in stats)
                {
                    int points = 999;
                    while(points > AP || points < 0){
                        Console.WriteLine("How many Attribute points would you like to give the {0} attribute?", item.Key);
                        points = Int32.Parse(Console.ReadLine());
                        if(points > AP || points < 0){
                            Console.WriteLine("ERROR: Invalid amount of Attribute Points");
                        }
                    }
                    statbuffs[idx] = points;
                    idx++;
                    //item.Value = points;
                    AP -= points;   
                }
                idx = 0;
                foreach(string key in keys){
                    stats[key] += statbuffs[idx];
                    idx++;
                }
                foreach (KeyValuePair<string, int> item in stats)
                {
                    Console.WriteLine("{0}: {1}", item.Key, item.Value);
                }
            }
        }
        public void BreakFist(){
            Weapon BF = new BrokenFists();
            BF.Create();
            WeaponList[0] = BF;
        }
        public void HealFist(){
           Weapon Fist = new Fists();
            Fist.Create();
            WeaponList[0]=Fist;
        }
        public void NewSpells(int magic){
            MP += (4*Lvl);
            //put higher level spells on top lower on bottom, always return at the bottom of each if. 
            if(magic >= 3){
                Console.WriteLine("You have gained the wisdom of the Fire Ball Spell, do you wish to learn it? [y/n]");
                string r  = Console.ReadLine();
                if(r == "y"){
                    Spell fb = new FireBall();
                    fb.Create();
                    LearnSpell(fb);
                }
                return;
            }
        }
        public void LearnSpell(Spell NewSpell){
            for(int i = 0; i < SpellBook.Length; i++){ //replace Empty slot
                if(SpellBook[i].name == ""){
                    SpellBook[i] = NewSpell;
                    return;
                }
            }
            //if we get here then all slots are filled
            Console.WriteLine("You are remembering as many spells as you can, you must forget one.");
            Console.WriteLine("Which Spell do you want to forget?");
            int index = 1; //what number spell is it
            for(int i = 0; i < SpellBook.Length; i++){
                if(SpellBook[i].name != ""){
                    Console.WriteLine("{0}. {1}", index, SpellBook[i].name);
                    index++;
                }
            }
            Console.WriteLine("6. Don't pick up the {0}", NewSpell.name);
            string selStr = Console.ReadLine();
            int selInt = Convert.ToInt32(selStr);
            if(selInt == 6){
                return;
            }
            selInt--;
            Console.WriteLine("You forgot {0} but learned {1}!", SpellBook[selInt].name, NewSpell.name);
            SpellBook[selInt] = NewSpell;
        }
    }
}