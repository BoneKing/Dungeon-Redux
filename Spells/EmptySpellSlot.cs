using System;

namespace Spells
{
    public class EmptySpellSlot : Spell
    {
        public override void Create()
        {
            this.name = "";
            this.baseDmg = 0;
            this.MPCost = 0;
            this.accuracy = 0;
            this.description = "";
        }
    }
}