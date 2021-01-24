using System;
using Weapons;

namespace Enemies
{
    public class Goblin : Enemy {
        Random random;
        public override void Create(){
            this.name = "Goblin";
            this.area = 2;
            this.health = 15;
            this.attackDmg = 7;
            this.speed = 4;
            this.dropRate = 80;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = attackDmg + random.Next(-6,1);
            Console.WriteLine("The Goblin stabs you with his pointed stick dealing {0} damage", dmg);
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You hit the Goblin, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Goblin has been killed");
            }
        }
        public override int DropItem(){
            random = new Random();
            if(random.Next(0,dropRate) <= dropRate){
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
            if(random.Next(0, 100) <= dropRate){
                Weapon wg = new PointedStick();
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