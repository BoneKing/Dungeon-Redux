using System;
using Weapons;

namespace Enemies
{
    public class Peter: Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 3: Peter the Pterodactyl";
            this.area = 3;
            this.health = 1;
            this.attackDmg = 29;
            this.speed = 100;
            this.dropRate = 101; //out of 100
            chargeCounter = 0;
            Console.WriteLine("The Pterodactyl is wearing sunglasses and seems like a pretty cool dude.");
            Console.WriteLine("*Hello I'm Peter, Peter the Pterodactyl. I am the guardian of the next area of Hell.* says Peter the Pterodactyl");
            Console.WriteLine("*My test is not one of physical strength*");
            Console.WriteLine("*You will need command over language and wit*");
            Test();
        }
        public override int getHealth(){
            return health;
        }
        public void Test(){
            Console.WriteLine("*Your test is... spell the word Pterodactyl*");
            string ans = Console.ReadLine();
            if(ans == "Pterodactyl"){
                Console.WriteLine("*WOW! Amazing! You may pass!*");
                Console.WriteLine("*But my Boss doesn't know I'm doing this, so just hit me, dont't worry I can take it.*");
            }
            else{
                Console.WriteLine("*HAHA no one ever gets that right!*");
                Console.WriteLine("*Welp, time to kill.*");
                Console.WriteLine("Peter opens his mouth to reveal a pitch black void of insanity, you try to fight back from being sucked in");
                health = 999;
            }
        }
        public override int Attack(){
            int dmg = 9999;
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You tap Peter, it does 1 damage");
            if(health < 1){
                Console.WriteLine("Peter winks and plays dead, the bridge is now free and clear to cross");
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