using System;

namespace Weapons
{
    public class Fists : Weapon{
        public override void Create(){
            this.name = "Fist";
            this.baseDmg = 4;
            this.lowRange = -1;
            this.highRange = 1;
            this.durability = 999;
        }
    }
}