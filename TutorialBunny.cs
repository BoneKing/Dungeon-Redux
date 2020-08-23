using System;

namespace Dungeon_Redux
{
    public class TutorialBunny : Enemy{
        Random random;
        public void NewBunny(){
            this.health = 5;
            this.attackDmg = 1;
            this.name = "Tutorial Bunny";
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            random = new Random();
            int dmg = attackDmg + random.Next(0,2);
            Console.WriteLine("The Tutorial Bunny Lunges at you in self defense dealing {0} damage", dmg);
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You hit the Tutrial Bunny, it does {0} damage", damage);
            if(health < 1){
                Console.WriteLine("The bunny has been slaughtered, good for you.");
            }
        }
    }
}