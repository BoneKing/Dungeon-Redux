using System;
using Weapons;

namespace Enemies
{
    public class SavageBarbarian : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Savage Barbarian";
            this.area = 3;
            this.health = 20;
            this.attackDmg = 10;
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
                Console.WriteLine("The {0} dances around looking for a place to strike, but trips and falls, dealing 4 damage to himself", name);
                health-=4;
                if(health < 1){
                    Console.WriteLine("The Savage's fall looks to have knocked him unconscious, I guess we won?");
                }
                return 0;
            }
            else{
                dmg = attackDmg + random.Next(-8,2);
                Console.WriteLine("The {0} pokes you with his pointed stick dealing {1} damage", name, dmg);
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
            Weapon wg = new EmptyWeaponSlot();
            wg.Create();
            return wg;
        }
    }
}