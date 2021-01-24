using System;

namespace Weapons
{
    public class TuskDagger : Weapon{
        public override void Create(){
            this.name = "Tusk Dagger";
            this.baseDmg = 5;
            this.lowRange = 0;
            this.highRange = 2;
            this.durability = 4;
        }
    }
}