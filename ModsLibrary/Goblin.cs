using System;
using LabaOOP2;
using LabaOOP2.Skills;

namespace ModsLibrary
{
    public class Goblin : Unit
    {
        public Goblin() : base("Goblin", 8, new damage(2, 5), 4, 4, 12)
        {
            PassiveSkills.Add(new Recharge(true));
        }
    }
}
