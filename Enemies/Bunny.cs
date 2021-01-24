using System;
using Weapons;

namespace Enemies
{
    public class Bunny : Enemy{
        Random random;
        public override void Create(){
            this.health = 5;
            this.attackDmg = 1;
            this.name = "Bunny";
            this.area = 1;
            this.dropRate = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = attackDmg + random.Next(0,2);
            Console.WriteLine("The Bunny Lunges at you in self defense dealing {0} damage", dmg);
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You hit the Bunny, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Bunny has been slaughtered, good for you.");
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
             Weapon wg = new EmptyWeaponSlot();
            wg.Create();
            return wg;
        }
    }
}