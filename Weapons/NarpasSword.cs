using System;

namespace Weapons
{
    public class NarpasSword : Weapon{
        public override void Create(){
            this.name = "Narpa's Sword";
            this.baseDmg = 999;
            this.lowRange = 0;
            this.highRange = 0;
            this.durability = 999;
        }
    }
}