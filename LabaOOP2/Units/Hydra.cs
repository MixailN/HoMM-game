using System;
using System.Collections.Generic;
using System.Text;
using LabaOOP2.Skills;

namespace LabaOOP2.Units
{
    class Hydra : Unit
    {
        public Hydra() : base("Hydra", 80, new damage(7, 14), 15, 12, 7)
        {
            PassiveSkills.Add(new Recharge(true));
            ActiveSkills.Add(new InitiativeBaff());
        }
    }
}
