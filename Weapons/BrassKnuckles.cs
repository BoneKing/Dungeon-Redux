using System;

namespace Weapons
{
    public class BrassKnuckles : Weapon{
        public override void Create(){
            this.name = "Brass Knuckles";
            this.baseDmg = 20;
            this.lowRange = -2;
            this.highRange = 2;
            this.durability = 8;
        }
    }
}