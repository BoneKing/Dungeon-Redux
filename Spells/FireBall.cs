using System;

namespace Spells
{
    public class FireBall : Spell
    {
        public override void Create()
        {
            this.name = "Fire Ball";
            this.baseDmg = 5;
            this.MPCost = 5;
            this.accuracy = 95;
            this.description = "Cost = 5 MP \t Damage = 5 \t Accuracy = 95 \t shoots a fire ball from your hand.";
        }
    }
}