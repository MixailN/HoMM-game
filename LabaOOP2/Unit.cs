using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public struct damage
    {
        public int min;
        public int max;
        public damage(int nmin, int nmax)
        {
            this.min = nmin;
            this.max = nmax;
        }
    }
    public class Unit
    {
        public string Type { get; }
        public int HitPoints { get; }
        public int Attack { get; }
        public damage Damage { get; }
        public int Defence { get; }
        public double Initiative { get; }

        public List<Skill> PassiveSkills { get; } = new List<Skill>();

        public List<Skill> ActiveSkills { get; } = new List<Skill>();
        public Unit(string nType, int nHP, damage nDamage, int nAttack, int nDefence, double nInitiative)
        {
            Type = nType;
            HitPoints = nHP;
            Damage = nDamage;
            Attack = nAttack;
            Defence = nDefence;
            Initiative = nInitiative;
        }
      
    }
}
