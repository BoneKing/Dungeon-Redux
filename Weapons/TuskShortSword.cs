using System;

namespace Weapons
{
    public class TuskShortSword : Weapon{
        public override void Create(){
            this.name = "Tusk Short Sword";
            this.baseDmg = 13;
            this.lowRange = -5;
            this.highRange = 7;
            this.durability = 12;
        }
    }
}