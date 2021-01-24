using System;
using Weapons;

namespace Enemies
{
    public abstract class Enemy{
        public string name;
        public int area;
        public int health;
        public int attackDmg;
        public int dmg; //attackDmg + random range
        public int speed;
        public int dropRate;
        public abstract void Create();
        public abstract int getHealth();
        public abstract int Attack();
        public abstract void takeDamage(int damage);
        public abstract int DropItem();
        public abstract Weapon DropWeapon();
    }
}