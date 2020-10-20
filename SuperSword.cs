using System;

namespace Dungeon_Redux
{
    public class SuperSword : Weapon{
        public override void Create(){
            this.name = "Super Sword";
            this.baseDmg = 999;
            this.lowRange = 0;
            this.highRange = 0;
            this.durability = 999;
        }
    }
}