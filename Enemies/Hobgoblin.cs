using System;
using Weapons;

namespace Enemies
{
    public class Hobgoblin : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "HobGoblin";
            this.area = 2;
            this.health = 20;
            this.attackDmg = 14;
            this.speed = 6;
            this.dropRate = 80; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(random.Next(0,10) == 5){
                Console.WriteLine("The Hobgoblin leaps into the air and swings his bronze sword at your head!");
                Console.WriteLine("It hits dealing a massive 30 damage!");
                return 30;
            }
            else if(random.Next(1,3) == 2){
                Console.WriteLine("The Hobgoblin swings around his broze sword but misses");
                return 0;
            }
            else{
                dmg = attackDmg + random.Next(-10,2);
                Console.WriteLine("The Hobgoblin sloppily swings his bronze sword at you dealing {0} damage", dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the Hobgoblin, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Hobgoblin falls to the ground, you've defeated the Hobgoblin!");
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
                Weapon wg = new BronzeSword();
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