using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public class UnitsStack
    {
        public Unit Character { get; }
        public int Quantity { get; }
        public List<Skill> PassiveSkills { get; } = new List<Skill>();
        public List<Skill> ActiveSkills { get; } = new List<Skill>();
        public UnitsStack(Unit nCharacter, int nQuantity)
        {
            if(nQuantity <= 999999 && nQuantity > 0)
            {
                Character = nCharacter;
                Quantity = nQuantity;
                PassiveSkills = nCharacter.PassiveSkills;
                ActiveSkills = nCharacter.ActiveSkills;
            }
            else
            {
                throw new Exception("Unit's quantity must be less then 999999");
            }
        }

    }
}
