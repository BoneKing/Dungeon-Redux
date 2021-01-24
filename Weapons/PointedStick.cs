using System;

namespace Weapons
{
    public class PointedStick : Weapon{
        public override void Create(){
            this.name = "Pointed Stick";
            this.baseDmg = 7;
            this.lowRange = -2;
            this.highRange = 1;
            this.durability = 3;
        }
    }
}