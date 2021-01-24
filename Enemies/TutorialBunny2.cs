using System;
using Weapons;

namespace Enemies
{
    public class TutorialBunny2: Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 5: Tutorial Bunny";
            this.area = 4;
            this.health = 66;
            this.attackDmg = 45;
            this.speed = 100;
            this.dropRate = 101; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter > 0){
                chargeCounter = 0;
                Console.WriteLine("Tutorial Bunny punches you, just as you punched it.");
                Console.WriteLine("Dealing 40 damage!");
                dmg = 40;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("Tutorial Bunny has a look on its face, as if it's remembering something from when it was alive.");
                }
                else{
                    dmg = attackDmg + random.Next(-5,2);
                    Console.WriteLine("Tutorial Bunny jumps on you, dealing {0} damage!", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You smite the Tutorial Bunny, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Tutorial Bunny Has been Killed");
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