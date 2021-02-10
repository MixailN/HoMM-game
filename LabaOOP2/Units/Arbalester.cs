using System;
using System.Collections.Generic;
using System.Text;
using LabaOOP2.Skills;

namespace LabaOOP2.Units
{
    class Arbalester : Unit
    {
        public Arbalester() : base("Arbalester", 10, new damage (2, 8), 4, 4, 8)
       {
            PassiveSkills.Add(new Recharge(false));
            PassiveSkills.Add(new DisableRecharge());
            PassiveSkills.Add(new DisableDefence());
            
       }
    }
}
