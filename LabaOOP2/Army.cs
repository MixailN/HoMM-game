using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LabaOOP2
{
    public class Army
    {
        private List<UnitsStack> unitsStacks = new List<UnitsStack>();
        public Army() { }
        public IEnumerable<UnitsStack> Stacks => unitsStacks.AsReadOnly();
        public void AddStack(UnitsStack unitsStack)
        {
            if(unitsStacks.Count < 6)
            {
                unitsStacks.Add(unitsStack);
            }
            else
            {
                throw new Exception("Army can't contain more then 6 stacks");
            }
        }
       
    }
}
