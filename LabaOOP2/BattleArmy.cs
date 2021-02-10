using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public class BattleArmy
    {
        private List<BattleUnitsStack> unitsStacks { get; } = new List<BattleUnitsStack>();

        public IEnumerable<BattleUnitsStack> Stacks => unitsStacks.AsReadOnly();

        public int ArmyID;

        public BattleUnitsStack this[int index]
        {
            get
            {
                return unitsStacks[index];
            }
        }

        public BattleArmy(Army army, int nArmyID) 
        {
            ArmyID = nArmyID;
            foreach (var s in army.Stacks)
            {
                AddStack(new BattleUnitsStack(s, ArmyID));
            }
        }

        public bool isDead()
        {
            if(this.unitsStacks.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddStack(BattleUnitsStack unitsStack)
        {
            if (unitsStacks.Count < 9)
            {
                unitsStacks.Add(unitsStack);
            }
            else
            {
                throw new Exception("BattleArmy can't contain more then 9 stacks");
            }
        }
        public void RemoveStack(BattleUnitsStack unitsStack)
        {
            unitsStacks.Remove(unitsStack);
        }
    }
}
