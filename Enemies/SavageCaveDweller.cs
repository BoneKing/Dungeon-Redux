using System;
using Weapons;

namespace Enemies
{
    public class SavageCaveDweller : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Savage Cave Dweller";
            this.area = 2;
            this.health = 20;
            this.attackDmg = 20;
            this.speed = 5;
            this.dropRate = 80; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(random.Next(1,3) == 2){
                Console.WriteLine("The {0} dances around looking for a place to strike, but trips and falls, dealing 10 damage to himself", name);
                health-=10;
                if(health < 1){
                    Console.WriteLine("The Savage's fall looks to have knocked him unconscious, I guess we won?");
                }
                return 0;
            }
            else{
                dmg = attackDmg + random.Next(-8,2);
                Console.WriteLine("The {0} swings at you with his rusted sword dealing {1} damage", name, dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the Savage, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The savage falls to the ground, and runs on all fours away!");
            }
        }
        public override int DropItem(){
            random = new Random();
            if(random.Next(0, 100) <= dropRate){
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
            Weapon wg = new DwellerSword();
            wg.Create();
            return wg;
        }
    }
}