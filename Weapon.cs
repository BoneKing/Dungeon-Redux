using System;

namespace Dungeon_Redux
{
    public abstract class Weapon{
        public string name;
        public int baseDmg;
        public int lowRange;
        public int highRange;
        public int durability;
        public abstract void Create();
    }
}