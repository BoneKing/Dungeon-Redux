using System;

namespace Weapons
{
    public class Whip : Weapon{
        public override void Create(){
            this.name = "Snake Skin Whip";
            this.baseDmg = 3;
            this.lowRange = -2;
            this.highRange = 7;
            this.durability = 6;
        }
    }
}