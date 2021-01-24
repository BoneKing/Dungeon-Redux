using System;
using Weapons;

namespace Enemies
{
    public class Wolf : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Wolf";
            this.area = 1;
            this.health = 8;
            this.attackDmg = 9;
            this.speed = 5;
            this.dropRate = 95; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter == 1){
                chargeCounter = 0;
                Console.WriteLine("The Wolf Lunges for your neck dealing 12 damage");
                dmg = 12;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("The Wolf howls! Watch out he looks like he's going to do something rash");
                }
                else{
                    dmg = attackDmg + random.Next(-2,4);
                    Console.WriteLine("The Wolf tries to bite you, dealing {0} damage", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the Wolf, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Wolf lays motionless on the ground");
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