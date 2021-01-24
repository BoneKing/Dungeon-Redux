using System;
using Weapons;

namespace Enemies
{
    public class Pheonix : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Pheonix";
            this.area = 4;
            this.health = 15;
            this.attackDmg = 19;
            this.speed = 15;
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
                Console.WriteLine("The Pheonix is engulfed in a growing ball of fire! It looks like the Sun!");
                Console.WriteLine("It hits dealing a massive 30 damage! The Pheonix also looks completely reborn!");
                health = 15;
                chargeCounter = 0;
                return 33;
            }
            else{
                dmg = attackDmg + random.Next(0,2);
                Console.WriteLine("The Pheonix sends out a flaming feathers from its wings, dealing {0} damage", dmg);
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smited the Pheonix out of the sky! It does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Pheonix turns to ash, you've defeated the Pheonix! ...For now.");
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
                Weapon wg = new FlamingFeatherDarts();
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