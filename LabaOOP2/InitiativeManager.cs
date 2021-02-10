using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{   
    class LessCompare : IComparer<BattleUnitsStack>
    {
        public int Compare(BattleUnitsStack x, BattleUnitsStack y)
        {
            if(x.curInitiative > y.curInitiative)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    class LargerCompare : IComparer<BattleUnitsStack>
    {
        public int Compare(BattleUnitsStack x, BattleUnitsStack y)
        {
            if (x.curInitiative < y.curInitiative)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
    class InitiativeManager
    {
        private List<BattleUnitsStack> WaitingQueue = new List<BattleUnitsStack>();

        private List<BattleUnitsStack> BaseQueue = new List<BattleUnitsStack>();

        private static InitiativeManager Data;
        private InitiativeManager() { }
        public void CreateQueue(BattleArmy army1, BattleArmy army2)
        {
            foreach(var i in army1.Stacks)
            {
                AddToBase(i);
            }
            foreach (var i in army2.Stacks)
            {
                AddToBase(i);
            }
            BaseQueue.Sort(new LessCompare());
        }

        public BattleUnitsStack GetNext()
        {
            if (BaseQueue.Count != 0)
            {
                BattleUnitsStack tmp = BaseQueue[0];
                return tmp;
            } 
            else if (WaitingQueue.Count != 0)
            {
                BattleUnitsStack tmp = WaitingQueue[0];
                return tmp;
            }
            else
            {
                return null;
            }
        }

        public void AddToWaiting(BattleUnitsStack unitsStack)
        {
            if(unitsStack.curQueue == "Base")
            {
                unitsStack.curQueue = "Wait";
                BaseQueue.Remove(unitsStack);
                WaitingQueue.Add(unitsStack);
                WaitingQueue.Sort(new LargerCompare());
            }
            else
            {
                throw new Exception("Can't wait twice!");
            }
               
        }

        public void AddToBase(BattleUnitsStack unitsStack)
        {
            unitsStack.curQueue = "Base";
            BaseQueue.Add(unitsStack);
            BaseQueue.Sort(new LessCompare());
        }

        public void RemoveStack(BattleUnitsStack unitsStack)
        {
            if(BaseQueue.Contains(unitsStack))
            {
                BaseQueue.Remove(unitsStack);
            }
            if(WaitingQueue.Contains(unitsStack))
            {
                WaitingQueue.Remove(unitsStack);
            }
        }

        public void SortQueues()
        {
            BaseQueue.Sort(new LessCompare());
            WaitingQueue.Sort(new LargerCompare());
        }

        public bool isEmpty()
        {
            if(BaseQueue.Count == 0 && WaitingQueue.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static InitiativeManager GetInitiativeManager()
        {
            if(InitiativeManager.Data == null)
            {
                InitiativeManager.Data = new InitiativeManager();
            }
            return InitiativeManager.Data;
        }
        public string ShowInitiative()
        {
            string answer = "Base queue: ";
            foreach (var i in BaseQueue)
            {
                answer += $"{i.pCharacter.Type}{i.ArmyID} ({i.curInitiative}) ";
            }
            answer += "\nWaiting queue: ";
            foreach (var i in WaitingQueue)
            {
                answer += $"{i.pCharacter.Type} ({i.curInitiative}) ";
            }
            return answer;
        }
    }
}
