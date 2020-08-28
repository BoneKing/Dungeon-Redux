using System;

namespace Dungeon_Redux
{
    public class Boar : Enemy{
        Random random;
        public void NewBoar(){
            this.name = "Boar";
            this.health = 12;
            this.attackDmg = 5;
            this.speed = 3;
            this.dropRate = 95; //out of 100
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = attackDmg + random.Next(-2,4);
            Console.WriteLine("The Boar charges at you in with blood lust in his eyes dealing {0} damage", dmg);
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the Boar, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The Boar has been slain!");
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
            if(random.Next(0,dropRate) <= dropRate){
                Weapon wg = new TuskDagger();
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