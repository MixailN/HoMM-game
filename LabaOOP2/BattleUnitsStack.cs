using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public class BattleUnitsStack
    {
        private Unit Character;
        public Unit pCharacter { get => Character; }

        private int Quantity;

        private int LastHP;

        private int currentAttack;

        private damage currentDamage;

        private int currentDefence;

        private double currentInitiative;

        private int TurnCounter;

        private string Queue;

        private List<Modifier> modifiers = new List<Modifier>();

        public List<Skill> PassiveSkills { get; } = new List<Skill>();

        public List<Skill> ActiveSkills { get; } = new List<Skill>();

        public int ArmyID { get; }

        public IEnumerable<Modifier> Array => modifiers.AsReadOnly();

        public string curQueue
        {
            get => Queue; set
            {
                Queue = value;
            }
        }

        public int curTurnCounter
        {
            get => TurnCounter; set
            {
                TurnCounter = value;
            }
        }

        public int curQuantity
        {
            get => Quantity; set
            {
                if (value <= 999999 && value >= 0) Quantity = value; else throw new Exception("Quantity must be in [0; 999999]");
            }
        }

        public int curLastHP
        {
            get => LastHP; set
            {
                if (value >= 0) LastHP = value; else /*if (value == 0) LastHP = Character.HitPoints; else*/ throw new Exception("LastHP can't be less then 0");
            }
        }

        public int curAttack
        {
            get => currentAttack; set
            {
                if (value > 0) currentAttack = value; else throw new Exception("Attack can't be less then 0");
            }
        }

        public damage curDamage
        {
            get => currentDamage; set
            {
                if (value.min > 0 && value.max >= value.min) currentDamage = value; else throw new Exception("Damage can't be less then 0");
            }
        }

        public int curDefence
        {
            get => currentDefence; set
            {
                if (value >= 0) currentDefence = value; else throw new Exception("Defence can't be less then 0");
            }
        }

        public double curInitiative
        {
            get => currentInitiative; set
            {
                if (value > 0) currentInitiative = value; else throw new Exception("Initiative can't be less then 0");
            }
        }

        public BattleUnitsStack(UnitsStack unitsStack, int ID)
        {
            ArmyID = ID;
            Character = unitsStack.Character;
            Quantity = unitsStack.Quantity;
            LastHP = unitsStack.Character.HitPoints;
            currentAttack = unitsStack.Character.Attack;
            currentDamage = unitsStack.Character.Damage;
            currentDefence = unitsStack.Character.Defence;
            currentInitiative = unitsStack.Character.Initiative;
            TurnCounter = 0;
            PassiveSkills = unitsStack.PassiveSkills;
            ActiveSkills = unitsStack.ActiveSkills;
        }

        public void AddModifier(Modifier nModifier)
        {
            modifiers.Add(nModifier);
            switch(nModifier.curType)
            {
                case Baff.Attack:
                    switch(nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            if(Convert.ToInt32(currentAttack * (1 + nModifier.curValue)) < 0)
                            {
                                nModifier.curAddedValue = currentAttack;
                                curAttack = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = Convert.ToInt32(currentAttack * nModifier.curValue);
                                curAttack = Convert.ToInt32(currentAttack * (1 + nModifier.curValue));
                            }
                            break;
                        case ValueType.Value:
                            if (Convert.ToInt32(currentAttack + nModifier.curValue) < 0)
                            {
                                nModifier.curAddedValue = currentAttack;
                                curAttack = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = Convert.ToInt32(nModifier.curValue);
                                curAttack = Convert.ToInt32(currentAttack + nModifier.curValue);
                            }
                            break;
                    }
                    break;
                case Baff.Damage:
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            currentDamage.min = Convert.ToInt32(currentDamage.min * (1 + nModifier.curValue));
                            currentDamage.max = Convert.ToInt32(currentDamage.max * (1 + nModifier.curValue));
                            break;
                        case ValueType.Value:
                            currentDamage.min = Convert.ToInt32(currentDamage.min + nModifier.curValue);
                            currentDamage.max = Convert.ToInt32(currentDamage.max + nModifier.curValue);
                            break;
                    }
                    break;
                case Baff.Defence:
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            if(Convert.ToInt32(currentDefence * (1 + nModifier.curValue)) < 0)
                            {
                                nModifier.curAddedValue = currentDefence;
                                curDefence = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = Convert.ToInt32(currentDefence * nModifier.curValue);
                                curDefence = Convert.ToInt32(currentDefence * (1 + nModifier.curValue));
                            }
                            break;
                        case ValueType.Value:
                            if(Convert.ToInt32(currentDefence + nModifier.curValue) < 0)
                            {
                                nModifier.curAddedValue = currentDefence;
                                curDefence = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = Convert.ToInt32(nModifier.curValue);
                                curDefence = Convert.ToInt32(currentDefence + nModifier.curValue);
                            }
                            break;
                    }
                    break;
                case Baff.Initiative:
                    InitiativeManager initiativeManager = InitiativeManager.GetInitiativeManager();
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            if((currentInitiative + currentInitiative * nModifier.curValue) < 0)
                            {
                                nModifier.curAddedValue = currentInitiative * nModifier.curValue;
                                curInitiative = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = currentInitiative * nModifier.curValue;
                                curInitiative = currentInitiative + currentInitiative * nModifier.curValue;
                            }
                            initiativeManager.SortQueues();
                            break;
                        case ValueType.Value:
                            if((currentInitiative + nModifier.curValue) < 0)
                            {
                                nModifier.curAddedValue = currentInitiative;
                                curInitiative = 0;
                            }
                            else
                            {
                                nModifier.curAddedValue = nModifier.curValue;
                                curInitiative = currentInitiative + nModifier.curValue;
                            }
                            initiativeManager.SortQueues();
                            break;
                    }
                    break;
                case Baff.Skill:
                    if(nModifier.curValue == 1)
                    {
                        nModifier.curAddedValue = 1;
                        nModifier.curSkill.curOnOff = true;
                    }
                    if(nModifier.curValue == 0)
                    {
                        nModifier.curAddedValue = 0;
                        nModifier.curSkill.curOnOff = false;
                    }
                    break;
            }
        }
       
        public void RemoveModifier(Modifier nModifier)
        {
            switch (nModifier.curType)
            {
                case Baff.Attack:
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            curAttack -= Convert.ToInt32(nModifier.curAddedValue);
                            break;
                        case ValueType.Value:
                            curAttack -= Convert.ToInt32(nModifier.curAddedValue);
                            break;
                    }
                    break;
                case Baff.Damage:
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            currentDamage.min = Convert.ToInt32(currentDamage.min / (1 + nModifier.curValue));
                            currentDamage.max = Convert.ToInt32(currentDamage.max / (1 + nModifier.curValue));
                            break;
                        case ValueType.Value:
                            currentDamage.min = Convert.ToInt32(currentDamage.min - nModifier.curValue);
                            currentDamage.max = Convert.ToInt32(currentDamage.max - nModifier.curValue);
                            break;
                    }
                    break;
                case Baff.Defence:
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            curDefence -= Convert.ToInt32(nModifier.curAddedValue);
                            break;
                        case ValueType.Value:
                            curDefence -= Convert.ToInt32(nModifier.curAddedValue);
                            break;
                    }
                    break;
                case Baff.Initiative:
                    InitiativeManager initiativeManager = InitiativeManager.GetInitiativeManager();
                    switch (nModifier.curValueType)
                    {
                        case ValueType.Percent:
                            curInitiative -= Convert.ToInt32(nModifier.curAddedValue);
                            initiativeManager.SortQueues();
                            break;
                        case ValueType.Value:
                            curInitiative -= Convert.ToInt32(nModifier.curAddedValue);
                            initiativeManager.SortQueues();
                            break;
                    }
                    break;
                case Baff.Skill:
                    if (nModifier.curAddedValue == 1)
                    {
                        nModifier.curSkill.curOnOff = false;
                    }
                    if (nModifier.curAddedValue == 0)
                    {
                        nModifier.curSkill.curOnOff = true;
                    }
                    break;
            }
            modifiers.Remove(nModifier);
        }

        public void CheckModifiers()
        {
            for(int i = modifiers.Count - 1; i >= 0; i--)
            {
                if(modifiers[i].curDuration != -1 && modifiers[i].curDuration <= curTurnCounter - modifiers[i].curTurn)
                {
                    this.RemoveModifier(modifiers[i]);
                }
            }
        }

        public void UsePassiveSkills(SkillContext Context)
        {
            for(int i = 1; i < PassiveSkills.Count; i++)
            {
                PassiveSkills[i].Effect(Context);
            }
        }

        public bool isDead()
        {
            if(curQuantity == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
