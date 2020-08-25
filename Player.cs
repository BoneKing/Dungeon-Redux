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
            Console.WriteLine("You have become Hungry");
            hungerCounter++;
            if(hungerCounter == 4){
                Console.WriteLine("You're starving!"); 
            }
            else if(hungerCounter > 4){
                Console.WriteLine("The pain in your stomach is too much to take, your vision begins to fade as you feel all energy being sapped from your body");
                die();
            }

        }
        public void eat(){ //decrements hunger counter
            hungerCounter--;
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
            return dmg;
        }
        public bool die(){
            Console.WriteLine("You Died. \n you killed {0} enemies", enemiesKilled);
            dead = true; 
            return getdead(); 
        }
    }
}