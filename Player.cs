using System;
using System.Collections; 
using System.Collections.Generic; 

namespace Dungeon_Redux
{
    public class Player{
        Random random;
        public int health; //players health
        int maxHealth; //max health
        public bool dead; //is the player dead
        public int attackDamage; //base attack damage
        public int numHealthPotions; //number of health potions held
        int healthPotionHealAmount; //how strong the health potion is
        int healthPotionDropChance; //how often health potions drop
        public int numFood; //how much food the player has
        int foodDropChance; //how often food drops
        public int enemiesKilled; //how many enemies have been defeated 
        public int stamina; //player stamina 
        public int speed; //speed stat to get away (maybe dodge later)
        public bool running; //can you run?
        public int hungerCounter; //how hungry you are
        public Weapon[] WeaponList = new Weapon[5];
        public int score;
        public int TotalHourAte; //day and hour as hours since eaten last
        public int waitHungerWarning;
        public void NewPlayer(){ //init player
            health = 100;
            maxHealth = 100;
            dead = false;
            attackDamage = 7;
            numHealthPotions = 3;
            healthPotionHealAmount = 35;
            healthPotionDropChance = 60; // percentage
            numFood = 0;
            foodDropChance = 40; // percentage
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
            score = 0;
            TotalHourAte = 0;
            waitHungerWarning = 0;
        }
        public bool getdead(){
            //Console.WriteLine("health = {0}", health);
            if (health < 1){
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
        public int Attack(int i){
            Weapon w = WeaponList[i];
            random = new Random();
            int dmg = w.baseDmg + random.Next(w.lowRange,w.highRange);
            w.durability--;
            if(w.durability <= 0){
                Console.WriteLine("\n{0} Broke.", w.name);
                WeaponList[i] = new EmptyWeaponSlot();
                WeaponList[i].Create();
                SortWeaponList();
            }
            return dmg;
        }
        public bool die(){
            Console.WriteLine("You Died. \n you killed {0} enemies", enemiesKilled);
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
    }
}