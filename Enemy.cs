using System;

namespace Dungeon_Redux
{
    public abstract class Enemy{
        public string name;
        public int health;
        public int attackDmg;
        public int dmg; //attackDmg + random range
        public int speed;
        public abstract int getHealth();
        public abstract int Attack();
        public abstract void takeDamage(int damage);
    }
}