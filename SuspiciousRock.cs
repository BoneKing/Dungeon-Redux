using System;

namespace Dungeon_Redux
{
    public class SuspicousRock : Enemy{
        Random random;
        public void NewRock(){
            this.name= "Suspicous Rock";
            this.health = 1;
            this.attackDmg = 0;
            this.speed = 0;
        }
        public override int getHealth(){
            return health;
        }
        public override int Attack(){
            int dmg = 0;
            random = new Random();
            int deadly = random.Next(500);
            if(deadly == 2){
                dmg = 999;
                Console.WriteLine("The Rock is not a Rock! I repeate The Rock is not a ROCK! {0} damage", dmg);
            }
            else{
                Console.WriteLine("Its a rock, it deals {0}", dmg);
            }
            //destroy weapon code here
            return dmg;
        }
        public override void takeDamage(int damage){
            health = health - damage;
            Console.WriteLine("You struck the {0}, it does {1} damage", name, damage);
            if(health < 1){
                Console.WriteLine("The {0} Shattered", name);
            }
        }
    }   
}