using System;
using Weapons;

namespace Enemies
{
    public class Cerberus: Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 3: Cerberus";
            this.area = 3;
            this.health = 38;
            this.attackDmg = 29;
            this.speed = 100;
            this.dropRate = 101; //out of 100
            chargeCounter = 0;
            Console.WriteLine("The beast growls and barks at you! This is gonna be tough one");
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter > 0){
                chargeCounter = 0;
                Console.WriteLine("Cerberus uses one of its heads to chomp down on you!");
                Console.WriteLine("Dealing 35 damage!");
                dmg = 35;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("Cerberus Pins you to the ground!");
                }
                else{
                    dmg = attackDmg + random.Next(-5,2);
                    Console.WriteLine("Cerberus Slams you with his huge paw, dealing {0} damage!", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smite Cerberus, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("Cerberus Winces and runs away, the bridge is now free and clear to cross");
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
            if(random.Next(0,100) <= dropRate){
                Weapon wg = new EmptyWeaponSlot();
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