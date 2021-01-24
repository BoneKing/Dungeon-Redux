using System;

namespace Weapons
{
    public class Mace : Weapon{
        public override void Create(){
            this.name = "Bear Claw Mace";
            this.baseDmg = 10;
            this.lowRange = -2;
            this.highRange = 4;
            this.durability = 10;
        }
    }
}