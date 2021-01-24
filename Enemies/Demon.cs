using System;
using Weapons;

namespace Enemies
{
    public class Demon : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Demon";
            this.area = 3;
            this.health = 25;
            this.attackDmg = 19;
            this.speed = 8;
            this.dropRate = 80; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(random.Next(0,15) == 5){
                Console.WriteLine("The Demon leaps into the air and lands ontop of you, and begins clawing into you!");
                Console.WriteLine("It hits dealing a massive 30 damage!");
                return 30;
            }
            else if(random.Next(1,5) == 2){
                Console.WriteLine("The Demon is just running around, its difficult to keep track of.");
                speed += 2;
                return 0;
            }
            else{
                dmg = attackDmg + random.Next(-2,2);
                Console.WriteLine("The Demon scratches you with its long claws, dealing {0} damage", dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smited the Demon! it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Demon turns to dust, you've defeated the Demon!");
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
                Weapon wg = new BrassKnuckles();
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