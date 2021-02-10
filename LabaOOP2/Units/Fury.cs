using System;
using System.Collections.Generic;
using System.Text;
using LabaOOP2.Skills;

namespace LabaOOP2.Units
{
    class Fury : Unit
    {
        public Fury() : base("Fury", 16, new damage(5, 7), 5, 3, 16)
        {
            PassiveSkills.Add(new Recharge(true));
            PassiveSkills.Add(new DisableRecharge());
            ActiveSkills.Add(new AttackBaff());
        }
    }
}
