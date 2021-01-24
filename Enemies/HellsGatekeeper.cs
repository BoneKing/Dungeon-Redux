using System;
using Weapons;

namespace Enemies
{
    public class HellsGatekeeper : Enemy{
        Random random;
        public int chargeCounter;
        public override void Create(){
            this.name = "Boss 2: Hell's Gate Keeper";
            this.area = 2;
            this.health = 28;
            this.attackDmg = 19;
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
                Console.WriteLine("The Gate Keeper Slams his hammer into the ground fracturing it and knocking you into the air and you fall to the gound!");
                Console.WriteLine("Dealing 30 damage!");
                dmg = 30;
            }
            else{ 
                if(random.Next(0,4) == 3){
                    chargeCounter++;
                    Console.WriteLine("The Gate Keeper raises his hammer up above his head, Brace yourself!");
                }
                else{
                    dmg = attackDmg + random.Next(-2,2);
                    Console.WriteLine("The Gate Keeper Swings his hammer at you dealing {0} damage!", dmg);
                }
            }
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You strike the Gate Keeper, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Gate Keeper screams and is consumed by flames, The Gate to Hell slowly opens");
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
                Weapon wg = new WarHammer();
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