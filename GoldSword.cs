using System;

namespace Dungeon_Redux
{
    public class GoldSword : Weapon{
        public override void Create(){
            this.name = "Gold Sword";
            this.baseDmg = 30;
            this.lowRange = -2;
            this.highRange = 4;
            this.durability = 17;
        }
    }
}