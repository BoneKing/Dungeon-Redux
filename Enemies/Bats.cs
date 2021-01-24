using System;
using Weapons;

namespace Enemies
{
    public class Bats : Enemy {
        Random random;
        public override void Create(){
            this.name = "Swarm of Bats";
            this.area = 1;
            this.health = 7;
            this.attackDmg = 2;
            this.speed = 7;
            this.dropRate = 90;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = attackDmg + random.Next(-1,2);
            Console.WriteLine("The Bats surround you {0} damage", dmg);
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You hit the Bats, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Bats fly away");
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