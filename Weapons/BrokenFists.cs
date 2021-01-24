using System;

namespace Weapons
{
    public class BrokenFists : Weapon{
        public override void Create(){
            this.name = "Broken Fist";
            this.baseDmg = 2;
            this.lowRange = -2;
            this.highRange = 0;
            this.durability = 999;
        }
    }
}