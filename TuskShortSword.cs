using System;

namespace Dungeon_Redux
{
    public class TuskShortSword : Weapon{
        public override void Create(){
            this.name = "Tusk Short Sword";
            this.baseDmg = 20;
            this.lowRange = -5;
            this.highRange = 7;
            this.durability = 12;
        }
    }
}