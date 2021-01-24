using System;
using Weapons;

namespace Enemies
{
    public class DemonGeneral : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Demon General";
            this.area = 4;
            this.health = 35;
            this.attackDmg = 21;
            this.speed = 10;
            this.dropRate = 80; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            int attackChoice = random.Next(0,3);
            if(attackChoice == 0){
                Console.WriteLine("The Demon General Shield Bashes you!");
                Console.WriteLine("It hits dealing a massive 20 damage!");
                return 20;
            }
            else if(attackChoice == 1){
                Console.WriteLine("The Demon General Slashes you with his Golden Sword");
                dmg = attackDmg + random.Next(-2,2);
                Console.WriteLine("Dealing {0} damage", dmg);
            }
            else{
                dmg = attackDmg + random.Next(-10,-3);
                Console.WriteLine("The Demon General punches you, dealing {0} damage", dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smited the Demon General! it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Demon General turns to dust leaving his armor and weapons behind, you've defeated the Demon General!");
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
                Weapon wg = new GoldSword();
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