using System;

namespace Weapons
{
    public class EmptyWeaponSlot : Weapon{
        public override void Create(){
            this.name = "Empty";
            this.baseDmg = 0;
            this.lowRange = 0;
            this.highRange = 0;
            this.durability = 999;
        }
    }
}