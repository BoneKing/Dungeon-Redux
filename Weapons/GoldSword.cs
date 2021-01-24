using System;

namespace Weapons
{
    public class GoldSword : Weapon{
        public override void Create(){
            this.name = "Gold Sword";
            this.baseDmg = 16;
            this.lowRange = -4;
            this.highRange = 4;
            this.durability = 17;
        }
    }
}