using System;
using Weapons;

namespace Enemies
{
    public class Dragon : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Dragon";
            this.area = 4;
            this.health = 40;
            this.attackDmg = 25;
            this.speed = 20;
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
                dmg = attackDmg + random.Next(-5,-5);
                Console.WriteLine("The Dragon Breathes fire at you!");
                Console.WriteLine("It hits dealing a massive {0} damage!", dmg);
                return 20;
            }
            else if(attackChoice == 1){
                Console.WriteLine("The Dragon blows you away by flapping its powerful wings");
                dmg = attackDmg + random.Next(-10,-5);
                Console.WriteLine("Dealing {0} damage", dmg);
            }
            else{
                dmg = attackDmg + random.Next(0,4);
                Console.WriteLine("The Dragon bashes me with its head, dealing {0} damage", dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smited the Dragon! it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Dragon falls out of the sky, you've defeated the Demon General!");
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