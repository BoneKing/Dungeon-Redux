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
    }
}