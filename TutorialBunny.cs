using System;

namespace Dungeon_Redux
{
    public class TutorialBunny : Enemy{
        public void NewBunny(){
            this.health = 5;
            this.attackDmg = 1;
            this.name = "Tutorial Bunny";
        }
        public int getHealth(){
            return health;
        }
        public int Attack(){
            Console.WriteLine("The Tutorial Bunny Lunges at you in self defense dealing {0} damage", attackDmg);
            return attackDmg;
        }
        public void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You hit the Tutrial Bunny, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The bunny has been slaughtered, good for you.");
            }
        }
    }
}