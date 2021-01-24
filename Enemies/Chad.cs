using System;
using Weapons;

namespace Enemies
{
    public class Chad: Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 4: Chad";
            this.area = 4;
            this.health = 50;
            this.attackDmg = 40;
            this.speed = 100;
            this.dropRate = 101; //out of 100
            chargeCounter = 0;
            Console.WriteLine("*So you've come to challenge me, Chad, Demon Lord, King of Hell!?*");
            Console.WriteLine("*Lets see what you've got!*");
            Console.WriteLine("Without wasting a moment Chad Leaps from his throne and lands in front of you!");
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter > 0){
                Console.WriteLine("Chad charges up his demonic power!");
                chargeCounter++;
                return 0;
            }
            if(chargeCounter > 4){
                chargeCounter = 0;
                Console.WriteLine("Chad unleashes a devestating blow");
                Console.WriteLine("Dealing 50 damage!");
                dmg = 50;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("Chad begins chanting demonic words! ...and something about tax evasion");
                }
                else{
                    dmg = attackDmg + random.Next(-10,0);
                    Console.WriteLine("Chad strikes you, dealing {0} damage!", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smite Chad, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("Chad is weak and bearly standing!");
                Console.WriteLine("*This isn't over! Not Yet! Tutorial Bunny! I Call you to me, your master, at once!*");
                Console.WriteLine("From the ground rises a Tutorial Bunny.");
                Console.WriteLine("Chad gives the little bunny some of his blood!");
                Console.WriteLine("The Bunny transforms into the buff Grotesque monster!");
                Console.WriteLine("The fight's not over yet!");
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
            if(random.Next(0,100) <= dropRate){
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