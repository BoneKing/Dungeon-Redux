using System;

namespace Weapons
{
    public class WarHammer : Weapon{
        public override void Create(){
            this.name = "War Hammer";
            this.baseDmg = 25;
            this.lowRange = 0;
            this.highRange = 6;
            this.durability = 20;
        }
    }
}