using System;
using Weapons;

namespace Enemies
{
    public class Bear : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 1: Bear";
            this.area = 1;
            this.health = 18;
            this.attackDmg = 10;
            this.speed = 999;
            this.dropRate = 95; //out of 100
            chargeCounter = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = 0;
            if(chargeCounter == 2){
                chargeCounter = 0;
                Console.WriteLine("The Bear slashes you with its huge claws knocking you to the ground, dealing 12 damage");
                dmg = 20;
            }
            else{ 
                if(chargeCounter > 0){
                    chargeCounter++;
                    Console.WriteLine("The Bear looks like he's going to strike");
                }
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("The Bear roars and stands up right!");
                }
                else{
                    dmg = attackDmg + random.Next(-1,4);
                    Console.WriteLine("The Boar charges at you in with blood lust in its eyes and foam coming from the mouth dealing {0} damage", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the Bear, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The stumbles and collapses to the ground!");
                Console.WriteLine("You see a path that goes even deeper into the cave, you hear some strange noises and decide to investegate.");
            }
        }
        public override int DropItem(){
            random = new Random();
            if(random.Next(0,100) <= dropRate){
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
            Weapon wg = new Mace();
            wg.Create();
            return wg;
        }
    }
}