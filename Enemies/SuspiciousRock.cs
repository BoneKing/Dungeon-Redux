using System;
using Weapons;

namespace Enemies
{
    public class SuspicousRock : Enemy{
        Random random;
        public override void Create(){
            this.name = "Suspicous Rock";
            this.area = 1;
            this.health = 1;
            this.attackDmg = 0;
            this.speed = 0;
            this.dropRate = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            int dmg = 0;
            random = new Random();
            int deadly = random.Next(500);
            if(deadly == 2){
                dmg = 999;
                area = 4;
                dropRate = 100;
                Console.WriteLine("The Rock is not a Rock! I repeate The Rock is not a ROCK! {0} damage", dmg);
            }
            else{
                Console.WriteLine("Its a rock, it deals {0}", dmg);
            }
            //destroy weapon code here
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the {0}, it does {1} damage", name, damage);
            if(health < 1){
                Console.WriteLine("The {0} Shattered", name);
            }
        }
        public override int DropItem(){
            random = new Random();
            if(random.Next(0, 100) <= dropRate){
                if(random.Next(0,50) <= 50){
                    return 1; //food
                }
                else {
                    return 1; //health Potion
                }
            }
            else{
                return 0; //nothing
            }
        }
        public override Weapon DropWeapon(){
            random = new Random();
            if(random.Next(0,dropRate) <= dropRate){
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