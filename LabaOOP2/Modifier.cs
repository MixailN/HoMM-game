using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public enum Baff
    {
        Attack,
        Damage,
        Defence,
        Initiative,
        Skill
    }
    public enum ValueType
    {
        Percent,
        Value
    }
    public class Modifier
    {
        private Baff Type;

        public Baff curType 
        { 
            get => Type; set
            {
                Type = value;
            }
           
        }
        private int Turn;
        public int curTurn
        {
            get => Turn; set
            {
                Turn = value;
            }

        }
        private int Duration;
        public int curDuration //If -1, constant; if 0, for one action; if another number temporary
        {
            get => Duration; set
            {
                Duration = value;
            }

        }
        private ValueType ValueType;
        public ValueType curValueType
        {
            get => ValueType; set
            {
                ValueType = value;
            }

        }
        private double Value;
        public double curValue
        {
            get => Value; set
            {
                Value = value;
            }

        }

        private Skill SkillRef;

        public Skill curSkill
        {
            get => SkillRef; set
            {
                SkillRef = value;
            }
        }

        private double AddedValue { get; set; } 

        public double curAddedValue
        {
            get => AddedValue; set
            {
                AddedValue = value;
            }
        }
        public Modifier(Baff nType, int nDuration, ValueType nValueType, double nValue, int nTurn)
        {
            Type = nType;
            Duration = nDuration;
            ValueType = nValueType;
            Value = nValue;
            Turn = nTurn;
        }
        public Modifier(Baff nType, int nDuration, int nTurn, Skill nSkillRef, double nValue)
        {
            Type = nType;
            Duration = nDuration;
            curSkill = nSkillRef;
            Turn = nTurn;
            Value = nValue;
        }
    }
}
