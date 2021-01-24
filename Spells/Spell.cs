using System;

namespace Spells
{
    public abstract class Spell{
        public string name; //name of spell
        public int baseDmg; //how much dmg it should do
        public int MPCost; //how much should it cost
        public int accuracy; //integer 1-100, how accurate 95 means it hits 95% of the time
        public string description; //a description to siplay, like MP cost dmg, accuracy, and flavor text 
        public abstract void Create();
    }
}