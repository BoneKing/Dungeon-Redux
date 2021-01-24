using System;
using Weapons;

namespace Enemies
{
    public class Snake : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Snake";
            this.area = 1;
            this.health = 16;
            this.attackDmg = 5;
            this.speed = 3;
            this.dropRate = 95; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter == 2){
                chargeCounter = 0;
                Console.WriteLine("The Snake crushes you, dealing 12 damage");
                dmg = 12;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    dmg = 4;
                    Console.WriteLine("The Snake wraps around you, and begins squeezing tighter and tighter, dealing {0} damage", dmg);
                }
                else{
                    dmg = attackDmg + random.Next(-2,4);
                    Console.WriteLine("The Snake hisses and lunges at you, trying to find a good place to bite,  dealing {0} damage", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the snake, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Snake falls limp, hey this could make a good whip!");
            }
        }
        public override int DropItem(){
            random = new Random();
            if(random.Next(0,dropRate) <= dropRate){
                if(random.Next(0,100) <= 50){
                    return 1; //food
                }
                else {
                    return 2; //health Potion
                }
            }
            else{
                return 0; //nothing
            }
        }
        public override Weapon DropWeapon(){
            random = new Random();
            if(random.Next(0, 100) <= dropRate){
                Weapon wg = new Whip();
                wg.Create();
                return wg;
            }
            else{
                Weapon wg = new EmptyWeaponSlot();
                wg.Create();
                return wg;
            }
        }
    }
}