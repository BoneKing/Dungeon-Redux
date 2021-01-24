using System;

namespace Weapons
{
    public class FlamingFeatherDarts : Weapon{
        public override void Create(){
            this.name = "Flaming Feather Darts";
            this.baseDmg = 10;
            this.lowRange = -5;
            this.highRange = 10;
            this.durability = 5;
        }
    }
}