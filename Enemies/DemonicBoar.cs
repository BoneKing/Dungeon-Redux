using System;
using Weapons;

namespace Enemies
{
    public class DemonicBoar : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Demonic Boar";
            this.area = 3;
            this.health = 18;
            this.attackDmg = 19;
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
            if(chargeCounter > 2){
                Console.WriteLine("The Demon Boar charges at you with its huge tusks, flames burning from its nostriles!");
                Console.WriteLine("It hits dealing a massive 33 damage!");
                chargeCounter = 0;
                return 33;
            }
            else{
                dmg = attackDmg + random.Next(-15,2);
                Console.WriteLine("The Boar runs you down, dealing {0} damage", dmg);
                chargeCounter++;
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smited the Demonic Boar! it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Demonic Boar bursts into flames and turns to ash, you've defeated the Demonic Boar!");
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
                Weapon wg = new TuskShortSword();
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