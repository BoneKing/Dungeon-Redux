using System;

namespace Dungeon_Redux
{
    public class Player{
        int health; //players health
        bool dead; //is the player dead
        int attackDamage; //base attack damage
        int numHealthPotions; //number of health potions held
        int healthPotionHealAmount; //how strong the health potion is
        int healthPotionDropChance; //how often health potions drop
        int numFood; //how much food the player has
        int foodDropChance; //how often food drops
        int enemiesKilled; //how many enemies have been defeated 
        int stamina; //player stamina 
        bool running; //can you run?
        int hungerCounter; //how hungry you are
        public void NewPlayer(){ //init player
            health = 100;
            dead = false;
            attackDamage = 50;
            numHealthPotions = 3;
            healthPotionHealAmount = 35;
            healthPotionDropChance = 60; // percentage
            numFood = 0;
            foodDropChance = 40; // percentage
            enemiesKilled = 0; // part of score
            stamina = 5;
            running = true;
            hungerCounter = 0;
        }
        public bool getdead(){
            return dead;
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
        public bool die(){
            Console.WriteLine("You Died. \n you killed {0} enemies", enemiesKilled);
            dead = true; 
            return getdead(); 
        }
    }
}