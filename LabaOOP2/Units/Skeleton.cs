using System;
using System.Collections.Generic;
using System.Text;
using LabaOOP2.Skills;

namespace LabaOOP2.Units
{
    class Skeleton : Unit
    {
        public Skeleton() : base("Skeleton", 5, new damage(1, 1), 1, 2, 10)
        {
            PassiveSkills.Add(new Recharge(true));
        }
    }
}
