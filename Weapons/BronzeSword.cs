using System;

namespace Weapons
{
    public class BronzeSword : Weapon{
        public override void Create(){
            this.name = "Bronze Sword";
            this.baseDmg = 13;
            this.lowRange = -2;
            this.highRange = 4;
            this.durability = 10;
        }
    }
}