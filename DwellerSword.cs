using System;

namespace Dungeon_Redux
{
    public class DwellerSword : Weapon{
        public override void Create(){
            this.name = "Dweller Sword";
            this.baseDmg = 25;
            this.lowRange = -2;
            this.highRange = 4;
            this.durability = 7;
        }
    }
}